using Microsoft.EntityFrameworkCore;
using TextBooker.DataAccess.Entities;

namespace TextBooker.DataAccess
{
	internal static class DataSeeder
	{
		public static void AddData(ModelBuilder builder)
		{
			AddEmailTeplates(builder);
		}

		private static void AddEmailTeplates(ModelBuilder builder)
		{
			builder.Entity<EmailTemplate>().HasData(
				new EmailTemplate()
				{
					Id = 1,
					Subject = "Invite to TextBooker",
					Body = "Hi! Please follow <a href=\"%host%/user/confirm?email=%email%&token=%token%\">this link</a> to verify your email address.",
					Importance = false
				},

				new EmailTemplate()
				{
					Id = 2,
					Subject = "Feedback from TextBooker",
					Body = "Name: %name%<br> Email: %email%<br> Message: %message%",
					Importance = false
				}
			);
		}
	}
}
