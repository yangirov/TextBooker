using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using AutoMapper;

using Masking.Serilog;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Loki;

using TextBooker.Api.Infrastructure;
using TextBooker.BusinessLogic;
using TextBooker.BusinessLogic.Services;
using TextBooker.Common;
using TextBooker.Common.Config;
using TextBooker.DataAccess;

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

			var dbSettings = VaultClient.GetData(Configuration.GetSection("Database").Get<DatabaseSettings>());
			services.AddSingleton(dbSettings);

			var jwtSettings = VaultClient.GetData(Configuration.GetSection("Jwt").Get<JwtSettings>());
			services.AddSingleton(jwtSettings);

			var emailSettings = VaultClient.GetData(Configuration.GetSection("Email").Get<EmailSettings>());
			services.AddSingleton(emailSettings);

			var googleSettings = VaultClient.GetData(Configuration.GetSection("Google").Get<GoogleSettings>());
			services.AddSingleton(googleSettings);

			var fileSettings = VaultClient.GetData(Configuration.GetSection("FileStore").Get<FileStoreSettings>());
			services.AddSingleton(fileSettings);

			var systemInfoSettings = VaultClient.GetData(Configuration.GetSection("SystemInfo").Get<SystemInfoSettings>());
			services.AddSingleton(systemInfoSettings);

			var logger = new LoggerConfiguration()
				.ReadFrom.Configuration(Configuration)
				.WriteTo.LokiHttp(
					new NoAuthCredentials(VaultClient.GetData(Configuration.GetValue<string>("Serilog:LokiUrl"))),
					new LogLabelProvider(Configuration, HostingEnvironment)
				)
				.Destructure.ByMaskingProperties("Password", "Token")
				.CreateLogger();

			services.AddSingleton<ILogger>(logger);

			services
				.AddMvcCore(options =>
				{
					options.Filters.Add(typeof(ModelValidationFilter));
				})
				.AddCors(o => o.AddPolicy("AllowAnyOrigin",
					builder =>
					{
						builder.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader();
					}))
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
					builder.EnableRetryOnFailure();
				});

				options.UseSnakeCaseNamingConvention();
				options.EnableSensitiveDataLogging(false);
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			}, dbSettings.PoolSize);

			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
					.Where(File.Exists).ToArray();

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

			var mapper = new MapperConfiguration(cfg => cfg.AddMaps(BusinessLogicAssembly.Value)).CreateMapper();
			services.AddSingleton(mapper);

			services.AddMetrics(Program.Metrics);

			services.AddSingleton<IVersionService, VersionService>();
			services.AddTransient<IMailSender, MailSender>();
			services.AddTransient<ICommonService, CommonService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<ISiteSettingsService, SiteSettingsService>();
			services.AddTransient<IPageService, PageService>();
			services.AddTransient<IBlockService, BlockService>();
			services.AddTransient<IFileService, FileService>();
			services.AddTransient<ISiteGenerator, SiteGenerator>();
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
				c.RoutePrefix = string.Empty;
				var swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
				c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1.0/swagger.json", Configuration.GetValue<string>("SystemInfo:Name"));
			});

			var filestorePath = Configuration.GetValue<string>("FileStore:BasePath");
			var basePath = Path.Combine(VaultClient.GetData(filestorePath));
			if (!Directory.Exists(basePath))
				Directory.CreateDirectory(basePath);

			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(basePath),
				HttpsCompression = HttpsCompressionMode.Compress
			});

			app.UseCors("AllowAnyOrigin");

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
