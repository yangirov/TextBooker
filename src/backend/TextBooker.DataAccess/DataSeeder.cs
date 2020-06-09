using Microsoft.EntityFrameworkCore;

using TextBooker.DataAccess.Entities;
using TextBooker.Utils;

namespace TextBooker.DataAccess
{
	internal static class DataSeeder
	{
		public static void AddData(ModelBuilder builder)
		{
			AddEmailTeplates(builder);
			AddSiteTemplate(builder);
		}

		private static void AddEmailTeplates(ModelBuilder builder)
		{
			var emails = FileSeeder.FromJson<EmailTemplate>("emails");
			builder.Entity<EmailTemplate>().HasData(emails);
		}

		private static void AddSiteTemplate(ModelBuilder builder)
		{
			var templates = FileSeeder.FromJson<Template>("templates");
			builder.Entity<Template>().HasData(templates);

			var sectionKeys = FileSeeder.FromJson<TemplateKey>("templateKeys");
			builder.Entity<TemplateKey>().HasData(sectionKeys);
		}
	}
}
