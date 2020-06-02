using System.Net.Http;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Serilog;
using TextBooker.BusinessLogic.Infrastructure;
using TextBooker.Common.Config;
using TextBooker.Contracts.Dto.User;
using TextBooker.Common.Enums;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class UserService : BaseService, IUserService
	{
		private readonly TextBookerContext db;
		private readonly JwtSettings jwtSettings;
		private readonly IMailSender mailSender;
		private readonly GoogleSettings googleOptions;
		private readonly IHttpClientFactory clientFactory;
		private readonly PasswordHasher passwordHasher;

		private readonly HttpContext httpContext;
		private readonly string baseUrl;

		public UserService(
			ILogger logger,
			TextBookerContext db,
			IMailSender mailSender,
			JwtSettings jwtSettings,
			GoogleSettings googleOptions,
			IHttpClientFactory clientFactory,
			IHttpContextAccessor httpContextAccessor
		) : base(logger, db)
		{
			this.db = db;
			this.mailSender = mailSender;
			this.jwtSettings = jwtSettings;
			this.googleOptions = googleOptions;
			this.clientFactory = clientFactory;
			httpContext = httpContextAccessor.HttpContext;
			baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}";

			var hashOptions = new HashingOptions();
			passwordHasher = new PasswordHasher(hashOptions);
		}

		public async Task<Result<UserInfoDto>> GetInfo(string userId)
		{
			return await FindUserById(userId)
				.Bind(user => MapUser(user))
				.OnFailure(LogError);

			static Result<UserInfoDto> MapUser(User user)
			{
				var userInfo = new UserInfoDto()
				{
					Username = user.UserName,
					Email = user.Email
				};

				return Result.Ok(userInfo);
			}
		}

		public async Task<Result<bool>> Register(SignDto dto)
		{
			return await ValidateDto(dto)
				.Bind(async () => await RecaptchaVerify(dto.Token))
				.Bind(FindUser)
				.Bind(RegisterUser)
				.Bind(user => GenerateToken(user))
				.Bind(async (response) => await SendMesssage(response))
				.OnFailure(LogError);

			async Task<Result> FindUser()
			{
				var user = await FindUserByEmail(dto.Email);
				return user.HasValue
					? Result.Failure<User>($"The user with this email already exists: {dto.Email}")
					: Result.Ok();
			}

			async Task<Result<User>> RegisterUser()
			{
				var user = new User()
				{
					EmailConfirmed = false,
					Email = dto.Email.ToLower(),
					PasswordHash = passwordHasher.Hash(dto.Password)
				};

				db.Users.Add(user);
				await db.SaveChangesAsync();

				return Result.Ok(user);
			}

			async Task<Result<bool>> SendMesssage(SignResponse response)
			{
				await mailSender.Send(EmailTemplateKeys.Invite, dto.Email, response);
				return Result.Ok(true);
			}
		}

		public async Task<Result<SignResponse>> Login(SignDto dto)
		{
			return await ValidateDto(dto)
				.Bind(async () => await RecaptchaVerify(dto.Token))
				.Bind(FindUser)
				.Bind(user => CheckUserPassword(user))
				.Bind(user => GenerateToken(user))
				.OnFailure(LogError);

			async Task<Result<User>> FindUser()
			{
				var user = await FindUserByEmail(dto.Email);
				return user.HasValue && user.Value.EmailConfirmed
					? Result.Ok(user.Value)
					: Result.Failure<User>($"The user with this email was not found or not activated: {dto.Email}");
			}

			Result<User> CheckUserPassword(User user)
			{
				var (verified, needsUpgrade) = passwordHasher.Check(user.PasswordHash, dto.Password);
				return ( !verified && !needsUpgrade )
					? Result.Failure<User>($"Incorrect email or password: {dto.Email}")
					: Result.Ok(user);
			}
		}

		public async Task<Result<bool>> ConfirmEmail(string email, string token)
		{
			return await FindUser()
				.Bind(user => CheckToken(user))
				.Bind(user => UpdateUser(user))
				.OnFailure(LogError);

			async Task<Result<User>> FindUser()
			{
				var user = await FindUserByEmail(email);
				return user.HasValue
					? Result.Ok(user.Value)
					: Result.Failure<User>($"The user with this email was not found or not activated: {email}");
			}

			Result<User> CheckToken(User user)
			{
				var result = AuthenticationHelper.ValidateJwtToken(token, jwtSettings);
				return result
					? Result.Ok(user)
					: Result.Failure<User>("Token is invalid");
			}

			async Task<Result<bool>> UpdateUser(User user)
			{
				user.EmailConfirmed = true;

				db.Users.Update(user);
				await db.SaveChangesAsync();

				return Result.Ok(true);
			}
		}

		public async Task<Result<bool>> Update(string userId, UserUpdateDto dto)
		{
			return await FindUserById(userId)
				.Bind(user => UpdateUser(user))
				.OnFailure(LogError);

			async Task<Result<bool>> UpdateUser(User user)
			{
				user.UserName = dto.Username;

				db.Users.Update(user);
				await db.SaveChangesAsync();

				return Result.Ok(true);
			}
		}

		public async Task<Result<bool>> Delete(string userId)
		{
			return await FindUserById(userId)
				.Bind(user => DeleteUser(user))
				.OnFailure(LogError);

			async Task<Result<bool>> DeleteUser(User user)
			{
				db.Users.Remove(user);
				await db.SaveChangesAsync();

				return Result.Ok(true);
			}
		}

		private static Result ValidateDto(SignDto dto)
		{
			var (_, isFailure, error) = Validate();
			return isFailure
				? Result.Failure(error)
				: Result.Ok();

			Result Validate() => GenericValidator<SignDto>.Validate(v =>
			{
				v.RuleFor(c => c.Email).EmailAddress().NotEmpty();
				v.RuleFor(c => c.Password).NotEmpty();
				v.RuleFor(c => c.Token).NotEmpty();
			}, dto);
		}

		private Result<SignResponse> GenerateToken(User user)
		{
			var token = AuthenticationHelper.GenerateJwtToken(user.Email, user.Id.ToString(), jwtSettings);
			LogAudit($"Successful generate token: {user.Email}");
			return Result.Ok(new SignResponse(token, user.Email, baseUrl));
		}

		private async Task<Result> RecaptchaVerify(string token) => await AuthenticationHelper.RecaptchaTokenVerify(clientFactory, googleOptions, token);
	}
}
