using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class BaseService
	{
		private readonly ILogger logger;
		private readonly TextBookerContext db;

		public BaseService(ILogger logger, TextBookerContext db)
		{
			this.logger = logger;
			this.db = db;
		}

		public async Task<Result<User>> FindUserById(string userId)
		{
			var user = await db.Users.SingleOrDefaultAsync(u => u.Id == new Guid(userId)) ?? Maybe<User>.None;
			return user.HasNoValue
				? Result.Failure<User>($"The user with this identifier was not found: {userId}")
				: Result.Ok(user.Value);
		}

		public async Task<Maybe<User>> FindUserByEmail(string email) => await db.Users.SingleOrDefaultAsync(u => u.Email == email.ToLower()) ?? Maybe<User>.None;

		public void LogError(string error) => logger.Error(error);

		public void LogAudit(string message) => logger.Information(message);
	}
}
