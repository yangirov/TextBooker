using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Serilog;
using TextBooker.BusinessLogic.Infrastructure;
using TextBooker.BusinessLogic.Services.Base;
using TextBooker.Contracts.Dto.Config;
using TextBooker.Contracts.Dto.User;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class UserService : BaseService, IUserService
	{
		private readonly ILogger logger;
		private readonly TextBookerContext db;
		private readonly JwtSettings jwtSettings;
		private readonly PasswordHasher passwordHasher;

		public UserService(ILogger logger, TextBookerContext db, JwtSettings jwtSettings) : base(logger, db)
		{
			this.logger = logger;
			this.db = db;
			this.jwtSettings = jwtSettings;

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
				.Bind(FindUser)
				.Bind(RegisterUser)
				.OnFailure(LogError);

			async Task<Result> FindUser()
			{
				var user = await FindUserByEmail(dto.Email);
				return user.HasValue
					? Result.Failure<User>($"The user with this email already exists: {dto.Email}")
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
			return await ValidateDto(dto)
				.Bind(FindUser)
				.Bind(user => CheckUserPassword(user))
				.Bind(user => GenerateToken(user))
				.OnFailure(LogError);

			async Task<Result<User>> FindUser()
			{
				var user = await FindUserByEmail(dto.Email);
				return user.HasNoValue
					? Result.Failure<User>($"The user with this email was not found: {dto.Email}")
					: Result.Ok(user.Value);
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

		public Task<Result<bool>> Delete(string userId)
		{
			throw new System.NotImplementedException();
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
			}, dto);
		}
	}
}
