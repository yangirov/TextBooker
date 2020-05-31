using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TextBooker.DataAccess
{
	public class TextBookerContextFactory : IDesignTimeDbContextFactory<TextBookerContext>
	{
		public TextBookerContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
				.AddJsonFile("contextSettings.json", false, true)
				.Build();

			var envFilepath = configuration.GetValue<string>("EnvFilepath") ?? null;
			if (!string.IsNullOrEmpty(envFilepath) && File.Exists(envFilepath))
			{
				DotNetEnv.Env.Load(envFilepath);
			}

			var builder = new DbContextOptionsBuilder<TextBookerContext>();

			var connectionOptions = configuration.GetValue<string>("Database:Options");
			var connectionString = Environment.GetEnvironmentVariable(connectionOptions);
			builder.UseNpgsql(connectionString, builder =>
			{
				builder.UseNetTopologySuite();
				builder.EnableRetryOnFailure();
			});

			builder.UseSnakeCaseNamingConvention();
			builder.EnableSensitiveDataLogging(false);
			builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

			return new TextBookerContext(builder.Options);
		}
	}
}
