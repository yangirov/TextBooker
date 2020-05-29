using System.Linq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;

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
				.ConfigureWebHostDefaults(builder =>
				{
					builder.UseStartup<Startup>();
				});
		}
	}
}	
