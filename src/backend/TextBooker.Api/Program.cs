using System;
using System.IO;
using System.Linq;
using System.Reflection;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TextBooker.Api
{
	public class Program
	{
		internal static IMetricsRoot Metrics { get; set; }

		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			Metrics = new MetricsBuilder()
				.OutputMetrics.AsPrometheusPlainText()
				.Configuration.Configure(p =>
				{
					p.DefaultContextLabel = "Application";
					p.GlobalTags.Add("app", "TextBooker.Api");
					p.Enabled = true;
					p.ReportingEnabled = true;
				})
				.Build();

			return Host
				.CreateDefaultBuilder(args)
				.ConfigureMetrics(Metrics)
				.UseMetrics(
					options =>
					{
						options.EndpointOptions = endpointsOptions =>
						{
							endpointsOptions.MetricsTextEndpointOutputFormatter = Metrics.OutputMetricsFormatters.First(p => p is MetricsPrometheusTextOutputFormatter);
						};
					})
				.UseContentRoot(Directory.GetCurrentDirectory())
				.ConfigureWebHostDefaults(builder =>
				{
					builder.ConfigureAppConfiguration(x =>
					{
						x.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
						x.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
						x.AddEnvironmentVariables();
						x.Build();
					});

					builder.UseStartup<Startup>();
				});
		}
	}
}
