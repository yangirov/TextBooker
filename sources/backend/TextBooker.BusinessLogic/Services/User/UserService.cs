using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using TextBooker.BusinessLogic.Services.Infrastructure;
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

		public UserService(ILogger logger, TextBookerContext db, IOptions<JwtSettings> jwtSettings)
		{
			this.logger = logger;
			this.db = db;
			this.jwtSettings = jwtSettings.Value;

			var hashOptions = new HashingOptions();
			passwordHasher = new PasswordHasher(hashOptions);
		}

		public async Task<Result<UserShortInfoDto>> UserInfo(ClaimsPrincipal claimsUser)
		{
			var email = claimsUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(email))
				return Result.Failure<UserShortInfoDto>($"The email cannot be empty");

			var user = await db.Users.SingleOrDefaultAsync(u => u.Email == email.ToLower());
			var dto = new UserShortInfoDto()
			{
				Username = user.UserName,
				Email = user.Email
			};

			return ( user == null )
				? Result.Failure<UserShortInfoDto>($"The user with this email was not found: {email}")
				: Result.Ok(dto);
		}

		public async Task<Result<string>> Register(SignDto dto)
		{
			return await ValidateDto()
				.Bind(FindUser)
				.Bind(Register)
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
				var user = await db.Users.SingleOrDefaultAsync(u => u.Email == dto.Email.ToLower());
				return (user != null )
					? Result.Failure<User>($"The user already exists: {dto.Email}")
					: Result.Ok();
			}

			async Task<Result<string>> Register()
			{
				var user = new User()
				{
					Email = dto.Email.ToLower(),
					PasswordHash = passwordHasher.Hash(dto.Password)
				};

				db.Users.Add(user);
				await db.SaveChangesAsync();

				return Result.Ok($"Registration was successful. Check your email address ({dto.Email}) to activate your account.");
			}
 		}

		public async Task<Result<SignResponse>> Login(SignDto dto)
		{
			return await ValidateDto()
				.Bind(FindUser)
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

			async Task<Result<User>> FindUser()
			{
				var user = await db.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
				return (user == null)
					? Result.Failure<User>($"The user with this email was not found: {dto.Email}")
					: Result.Ok(user);
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

		private static Result Validate(in SignDto dto)
		{
			return GenericValidator<SignDto>.Validate(v =>
			{
				v.RuleFor(c => c.Email).EmailAddress().NotEmpty();
				v.RuleFor(c => c.Password).NotEmpty();
			}, dto);
		}

		private void LogError(string error) => logger.Error(error);

		private void LogAudit(string message) => logger.Information(message);		
	}
}
