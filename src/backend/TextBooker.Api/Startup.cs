using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;

using Microsoft.AspNetCore.Http;
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

using AutoMapper;

using Serilog;
using Serilog.Sinks.Loki;

using TextBooker.Api.Infrastructure;
using TextBooker.Api.Infrastructure.Filters;
using TextBooker.BusinessLogic.Services;
using TextBooker.Common.Config;
using TextBooker.DataAccess;
using Textbooker.Utils;

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

			services.AddSingleton(Configuration);

			var dbSettings = OptionsClient.GetData(Configuration.GetSection("Database").Get<DatabaseSettings>());
			services.AddSingleton(dbSettings);

			var jwtSettings = OptionsClient.GetData(Configuration.GetSection("Jwt").Get<JwtSettings>());
			services.AddSingleton(jwtSettings);

			var emailSettings = OptionsClient.GetData(Configuration.GetSection("Email").Get<EmailSettings>());
			services.AddSingleton(emailSettings);

			var googleSettings = OptionsClient.GetData(Configuration.GetSection("Google").Get<GoogleSettings>());
			services.AddSingleton(googleSettings);

			var logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.WriteTo.LokiHttp(
					new NoAuthCredentials(OptionsClient.GetData(Configuration.GetValue<string>("Serilog:LokiUrl"))),
					new LogLabelProvider(Configuration, HostingEnvironment)
				)
				.CreateLogger();

			services.AddSingleton<ILogger>(logger);

			services
				.AddMvcCore(options =>
				{
					options.Filters.Add(new MiddlewareFilterAttribute(typeof(LocalizationPipelineFilter)));
					options.Filters.Add(typeof(ModelValidationFilter));
				})
				.AddCors()
				.AddControllersAsServices()
				.AddFormatterMappings()
				.AddNewtonsoftJson()
				.AddApiExplorer()
				.AddDataAnnotations();

			services
				.AddResponseCompression()
				.AddControllers();

			services
				.AddLocalization()
				.AddMemoryCache();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddHttpClient(HttpClientNames.GoogleRecaptcha);

			services.AddEntityFrameworkNpgsql().AddDbContextPool<TextBookerContext>(options =>
			{
				options.UseNpgsql(dbSettings.ConnectionString, builder =>
				{
					builder.UseNetTopologySuite();
					builder.EnableRetryOnFailure();
				});

				options.UseSnakeCaseNamingConvention();
				options.EnableSensitiveDataLogging(false);
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			}, dbSettings.PoolSize);

			services
				.AddHealthChecks()
				.AddDbContextCheck<TextBookerContext>();

			services
				.AddAuthorization()
				.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
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
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
						ClockSkew = TimeSpan.FromDays(30)
					};
				});

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

			var mapper = new MapperConfiguration(cfg => cfg.AddMaps("TextBooker.BusinessLogic")).CreateMapper();
			services.AddSingleton(mapper);

			services.AddMetrics(Program.Metrics);
			services.AddSingleton<IVersionService, VersionService>();
			services.AddTransient<IMailSender, MailSender>();
			services.AddTransient<ICommonService, CommonService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IEditorService, EditorService>();
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
				var swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
				c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1.0/swagger.json", Configuration.GetValue<string>("SystemInfo:Name"));
			});

			app.UseCors(builder => builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
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
