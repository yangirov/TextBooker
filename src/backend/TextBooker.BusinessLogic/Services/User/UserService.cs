using System.Security.Claims;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using TextBooker.BusinessLogic.Infrastructure;
using TextBooker.Contracts.Dto.Config;
using TextBooker.Contracts.Dto.User;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class UserService : IUserService
	{
		private readonly ILogger logger;
		private readonly TextBookerContext db;
		private readonly JwtSettings jwtSettings;
		private readonly PasswordHasher passwordHasher;

		public UserService(ILogger logger, TextBookerContext db, JwtSettings jwtSettings)
		{
			this.logger = logger;
			this.db = db;
			this.jwtSettings = jwtSettings;

			var hashOptions = new HashingOptions();
			passwordHasher = new PasswordHasher(hashOptions);
		}

		public async Task<Result<UserInfoDto>> GetInfo(ClaimsPrincipal claimsUser)
		{
			return await GetUserEmail(claimsUser)
				.Bind(email => FindUser(email))
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
			return await ValidateDto()
				.Bind(FindUser)
				.Bind(RegisterUser)
				.OnFailure(LogError);

			Result ValidateDto()
			{
				var (_, isFailure, error) = Validate(dto);
				return isFailure
					? Result.Failure(error)
					: Result.Ok();
			}

			async Task<Result> FindUser()
			{
				var user = await FindUserByEmail(dto.Email);
				return user.HasValue
					? Result.Failure<User>($"The user already exists: {dto.Email}")
					: Result.Ok();
			}

			async Task<Result<bool>> RegisterUser()
			{
				var user = new User()
				{
					Email = dto.Email.ToLower(),
					PasswordHash = passwordHasher.Hash(dto.Password)
				};

				db.Users.Add(user);
				await db.SaveChangesAsync();

				return Result.Ok(true);
			}
 		}

		public async Task<Result<SignResponse>> Login(SignDto dto)
		{
			return await ValidateDto()
				.Bind(async () => await FindUser(dto.Email))
				.Bind(user => CheckUserPassword(user))
				.Bind(user => GenerateToken(user))
				.OnFailure(LogError);

			Result ValidateDto()
			{
				var (_, isFailure, error) = Validate(dto);
				return isFailure
					? Result.Failure(error)
					: Result.Ok();
			}

			Result<User> CheckUserPassword(User user)
			{
				var (verified, needsUpgrade) = passwordHasher.Check(user.PasswordHash, dto.Password);
				return ( !verified && !needsUpgrade )
					? Result.Failure<User>($"Incorrect email or password: {dto.Email}")
					: Result.Ok(user);
			}

			Result<SignResponse> GenerateToken(User user)
			{
				var token = AuthenticationHelper.GenerateJwtToken(user.Email, user.Id, jwtSettings);
				LogAudit($"Successful login: {dto.Email}");
				return Result.Ok(new SignResponse(token, user.Email));
			}
		}

		public async Task<Result<bool>> Update(ClaimsPrincipal claimsUser, UserUpdateDto dto)
		{
			return await GetUserEmail(claimsUser)
				.Bind(email => FindUser(email))
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

		private static Result Validate(in SignDto dto)
		{
			return GenericValidator<SignDto>.Validate(v =>
			{
				v.RuleFor(c => c.Email).EmailAddress().NotEmpty();
				v.RuleFor(c => c.Password).NotEmpty();
			}, dto);
		}

		private async Task<Result<User>> FindUser(string email)
		{
			var user = await FindUserByEmail(email);
			return user.HasNoValue
				? Result.Failure<User>($"The user with this email was not found: {email}")
				: Result.Ok(user.Value);
		}

		private async Task<Maybe<User>> FindUserByEmail(string email) => await db.Users.SingleOrDefaultAsync(u => u.Email == email.ToLower()) ?? Maybe<User>.None;

		private Result<string> GetUserEmail(ClaimsPrincipal data)
		{
			var email = data.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Maybe<string>.None;
			return email.HasNoValue
				? Result.Failure<string>($"The email cannot be empty")
				: Result.Ok(email.Value);
		}

		private void LogError(string error) => logger.Error(error);

		private void LogAudit(string message) => logger.Information(message);
	}
}
