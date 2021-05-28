using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Serilog;
using Serilog.Sinks.Loki;

using TextBooker.Api.Infrastructure;
using TextBooker.Api.Infrastructure.Converters;
using TextBooker.Api.Infrastructure.Filters;
using TextBooker.BusinessLogic.Services;
using TextBooker.DataAccess;
using TextBooker.Contracts.Dto.Config;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
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
			if (HostingEnvironment.IsDevelopment())
			{
				var envFilepath = Configuration.GetValue<string>("EnvFilepath") ?? null;
				if (!string.IsNullOrEmpty(envFilepath) && File.Exists(envFilepath))
				{
					DotNetEnv.Env.Load(envFilepath);
				}
			}

			services.AddResponseCompression();
			services.AddControllers();

			var lokiUrl = Configuration.GetValue<string>("Serilog:LokiUrl");
			var logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.WriteTo.LokiHttp(new NoAuthCredentials(Environment.GetEnvironmentVariable(lokiUrl)), new LogLabelProvider(Configuration, HostingEnvironment))
				.CreateLogger();

			services.AddSingleton<ILogger>(logger);

			services.AddMvcCore(options =>
				{
					options.Filters.Add(new MiddlewareFilterAttribute(typeof(LocalizationPipelineFilter)));
					options.Filters.Add(typeof(ModelValidationFilter));
				})
				.AddAuthorization()
				.AddCors()
				.AddControllersAsServices()
				.AddFormatterMappings()
				.AddNewtonsoftJson()
				.AddApiExplorer()
				.AddDataAnnotations();

			services
				.AddCors()
				.AddLocalization()
				.AddMemoryCache();

			services.AddHttpContextAccessor();

			var connectionOptions = Configuration.GetValue<string>("Database:Options");
			var connectionString = Environment.GetEnvironmentVariable(connectionOptions);
			services.AddEntityFrameworkNpgsql().AddDbContextPool<TextBookerContext>(options =>
			{
				options.UseNpgsql(connectionString, builder =>
				{
					builder.UseNetTopologySuite();
					builder.EnableRetryOnFailure();
				});

				options.UseSnakeCaseNamingConvention();
				options.EnableSensitiveDataLogging(false);
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			}, 16);

			services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
			var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();

			services
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					var jwtKey = Environment.GetEnvironmentVariable(jwtSettings.Key);

					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtSettings.Issuer,
						ValidAudience = jwtSettings.Issuer,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
						ClockSkew = TimeSpan.FromDays(30)
					};
				});

			services.AddHealthChecks()
					.AddDbContextCheck<TextBookerContext>();

			services.AddMetrics(Program.Metrics);

			var swaggerTitle = Configuration.GetValue<string>("SystemInfo:Name");
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1.0", new OpenApiInfo { Title = swaggerTitle, Version = "v1.0" });

				var currentAssembly = Assembly.GetExecutingAssembly();
				var xmlDocs = currentAssembly
					.GetReferencedAssemblies()
					.Union(new AssemblyName[] { currentAssembly.GetName() })
					.Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
					.Where(f => File.Exists(f)).ToArray();

				Array.ForEach(xmlDocs, (d) =>
				{
					c.IncludeXmlComments(d);
				});

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						Array.Empty<string>()
					}
				});
			});

			services.AddSingleton<IJsonSerializer, CustomJsonSerializer>();
			services.AddSingleton<IVersionService, VersionService>();
			services.AddTransient<IUserService, UserService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				var swaggerJsonBasePath = env.IsProduction() ? "api" : string.Empty;
				c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1.0/swagger.json", Configuration.GetValue<string>("SystemInfo:Name"));
			});

			app.UseCors(builder => builder
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyHeader());

			app.UseResponseCompression();

			var headersOptions = new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor,
				RequireHeaderSymmetry = false,
				ForwardLimit = null
			};
			headersOptions.KnownNetworks.Clear();
			headersOptions.KnownProxies.Clear();
			app.UseForwardedHeaders(headersOptions);

			app.UseMetricsAllMiddleware();

			app.Use(async (context, next) =>
			{
				await next.Invoke();
				Middlewares.AutoDiscoverRoutes(context);
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("/health");
				endpoints.MapControllers();
			});
		}
	}
}
