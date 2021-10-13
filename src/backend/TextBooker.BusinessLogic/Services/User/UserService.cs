using System.Net.Http;
using System.Threading.Tasks;

using AutoMapper;

using CSharpFunctionalExtensions;

using FluentValidation;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Serilog;

using TextBooker.BusinessLogic.Infrastructure;
using TextBooker.Common.Config;
using TextBooker.Contracts.Dto.User;
using TextBooker.Contracts.Enums;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class UserService : BaseService, IUserService
	{
		private readonly IMapper mapper;
		private readonly TextBookerContext db;
		private readonly JwtSettings jwtSettings;
		private readonly IMailSender mailSender;
		private readonly GoogleSettings googleOptions;
		private readonly IHttpClientFactory clientFactory;
		private readonly PasswordHasher passwordHasher;

		private readonly HttpContext httpContext;
		private readonly string baseUrl;

		public UserService(
			IMapper mapper,
			ILogger logger,
			TextBookerContext db,
			IMailSender mailSender,
			JwtSettings jwtSettings,
			GoogleSettings googleOptions,
			IHttpClientFactory clientFactory,
			IHttpContextAccessor httpContextAccessor
		) : base(logger)
        {
			this.mapper = mapper;
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
				.Bind(MapUser)
				.OnFailure(LogError);

			Result<UserInfoDto> MapUser(User user)
			{
				var userInfo = mapper.Map<UserInfoDto>(user);
				return Result.Success(userInfo);
			}
		}

		public async Task<Result<bool>> Register(SignDto dto)
		{
			return await ValidateDto(dto)
				.Bind(RecaptchaVerify)
				.Bind(FindUser)
				.Bind(RegisterUser)
				.Bind(GenerateToken)
				.Bind(SendMesssage)
				.OnFailure(LogError);

			async Task<Result> FindUser()
			{
				var user = await FindUserByEmail(dto.Email);
				return user.HasValue
					? Result.Failure<User>($"The user with this email already exists: {dto.Email}")
					: Result.Success();
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

				return Result.Success(user);
			}

			async Task<Result<bool>> SendMesssage(SignResponse response)
			{
				await mailSender.Send(EmailTemplateKeys.Invite, dto.Email, response);
				return Result.Success(true);
			}
		}

		public async Task<Result<SignResponse>> Login(SignDto dto)
		{
			return await ValidateDto(dto)
				.Bind(RecaptchaVerify)
				.Bind(FindUser)
				.Bind(CheckUserPassword)
				.Bind(GenerateToken)
				.OnFailure(LogError);

			async Task<Result<User>> FindUser()
			{
				var user = await FindUserByEmail(dto.Email);
				return user.HasValue && user.Value.EmailConfirmed
					? Result.Success(user.Value)
					: Result.Failure<User>($"The user with this email was not found or not activated: {dto.Email}");
			}

			Result<User> CheckUserPassword(User user)
			{
				var (verified, needsUpgrade) = passwordHasher.Check(user.PasswordHash, dto.Password);
				return ( !verified && !needsUpgrade )
					? Result.Failure<User>($"Incorrect email or password: {dto.Email}")
					: Result.Success(user);
			}
		}

		public async Task<Result<bool>> ConfirmEmail(string email, string token)
		{
			return await FindUser()
				.Bind(CheckToken)
				.Bind(UpdateUser)
				.OnFailure(LogError);

			async Task<Result<User>> FindUser()
			{
				var user = await FindUserByEmail(email);
				return user.HasValue
					? Result.Success(user.Value)
					: Result.Failure<User>($"The user with this email was not found or not activated: {email}");
			}

			Result<User> CheckToken(User user)
			{
				var result = AuthenticationHelper.ValidateJwtToken(token, jwtSettings);
				return result
					? Result.Success(user)
					: Result.Failure<User>("Token is invalid");
			}

			async Task<Result<bool>> UpdateUser(User user)
			{
				user.EmailConfirmed = true;

				db.Users.Update(user);
				await db.SaveChangesAsync();

				return Result.Success(true);
			}
		}

		public async Task<Result<bool>> Update(string userId, UserUpdateDto dto)
		{
			return await FindUserById(userId)
				.Bind(UpdateUser)
				.OnFailure(LogError);

			async Task<Result<bool>> UpdateUser(User user)
			{
				user.UserName = dto.Username;

				db.Users.Update(user);
				await db.SaveChangesAsync();

				return Result.Success(true);
			}
		}

		public async Task<Result<bool>> Delete(string userId)
		{
			return await FindUserById(userId)
				.Bind(DeleteUser)
				.OnFailure(LogError);

			async Task<Result<bool>> DeleteUser(User user)
			{
				db.Users.Remove(user);
				await db.SaveChangesAsync();

				return Result.Success(true);
			}
		}

		public async Task<Result<User>> FindUserById(string userId)
		{
			var user = await db.Users
				.Include(x => x.Sites)
				.SingleOrDefaultAsync(u => u.Id == userId) ?? Maybe<User>.None;

			return user.HasNoValue
				? Result.Failure<User>($"The user with this identifier was not found: {userId}")
				: Result.Success(user.Value);
		}

		public async Task<Maybe<User>> FindUserByEmail(string email)
			=> await db.Users.SingleOrDefaultAsync(u => u.Email == email.ToLower()) ?? Maybe<User>.None;

		private static Result<SignDto> ValidateDto(SignDto dto)
		{
			var (_, isFailure, error) = Validate();
			return isFailure
				? Result.Failure<SignDto>(error)
				: Result.Success(dto);

			Result Validate()
			{
				return GenericValidator<SignDto>.Validate(v =>
				{
					v.RuleFor(c => c.Email).EmailAddress().NotEmpty();
					v.RuleFor(c => c.Password).NotEmpty();
					v.RuleFor(c => c.Token).NotEmpty();
				}, dto);
			}
		}

		private Result<SignResponse> GenerateToken(User user)
		{
			var token = AuthenticationHelper.GenerateJwtToken(user.Email, user.Id.ToString(), jwtSettings);
			LogAudit($"Successful generate token: {user.Email}");
			return Result.Success(new SignResponse(token, user.Email, baseUrl));
		}

		private async Task<Result> RecaptchaVerify(SignDto dto)
			=> await AuthenticationHelper.RecaptchaTokenVerify(clientFactory, googleOptions, dto.Token);
	}
}
