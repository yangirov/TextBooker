using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Sinks.Loki;

using TextBooker.Api.Infrastructure;

namespace TextBooker.Api
{
    public class Startup
    {
		public IWebHostEnvironment HostingEnvironment { get; private set; }
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
			HostingEnvironment = env;
		}

        public void ConfigureServices(IServiceCollection services)
        {
			services.AddControllers();

			var lokiUrl = Configuration.GetValue<string>("Serilog:LokiUrl");

			services.AddSingleton((ILogger) new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.WriteTo.LokiHttp(new NoAuthCredentials(lokiUrl), new LogLabelProvider(Configuration, HostingEnvironment))
				.CreateLogger());

			services.AddHealthChecks();

			services.AddMetrics(Program.Metrics);
		}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.Use(async (context, next) =>
			{
				await next.Invoke();
				Middlewares.AutoDiscoverRoutes(context);
			});

			app.UseMetricsAllMiddleware();

			app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
				endpoints.MapHealthChecks("/health");
				endpoints.MapControllers();
            });
        }	
	}
}
