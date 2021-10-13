using System.IO;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using TextBooker.Common.Config;
using TextBooker.Utils;

namespace TextBooker.DataAccess
{
	public class TextBookerContextFactory : IDesignTimeDbContextFactory<TextBookerContext>
	{
		public TextBookerContext CreateDbContext(string[] args)
		{
			var dbContextOptions = new DbContextOptionsBuilder<TextBookerContext>();
			dbContextOptions.UseNpgsql(GetConnectionString());
			dbContextOptions.UseSnakeCaseNamingConvention();

			return new TextBookerContext(dbContextOptions.Options);
		}

		private static string GetConnectionString()
		{
			var configuration = (IConfiguration)new ConfigurationBuilder()
				.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
				.AddJsonFile("contextSettings.json", false, true)
				.Build();

			var envFilepath = configuration.GetValue<string>("EnvFilepath") ?? null;
			if (!string.IsNullOrEmpty(envFilepath) && File.Exists(envFilepath))
			{
				DotNetEnv.Env.Load(envFilepath);
			}

			var dbSettings = VaultClient.GetData(configuration.GetSection("Database").Get<DatabaseSettings>());
			return dbSettings.ConnectionString;
		}
	}
}
