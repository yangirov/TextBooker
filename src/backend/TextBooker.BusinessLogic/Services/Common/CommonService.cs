using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Serilog;
using TextBooker.BusinessLogic.Infrastructure;
using TextBooker.Contracts.Dto;
using TextBooker.Common.Config;
using TextBooker.Common.Enums;
using TextBooker.DataAccess;
using Textbooker.Utils;

namespace TextBooker.BusinessLogic.Services
{
	public class CommonService : BaseService, ICommonService
	{
		private readonly IMailSender mailSender;
		private readonly IHttpClientFactory clientFactory;
		private readonly GoogleSettings googleOptions;
		private readonly SystemInfo systemInfo;

		public CommonService(
			ILogger logger,
			TextBookerContext db,
			IMailSender mailSender,
			IVersionService versionService,
			IConfiguration config,
			IHttpClientFactory clientFactory,
			GoogleSettings googleOptions) : base(logger, db)
		{
			this.mailSender = mailSender;
			this.clientFactory = clientFactory;
			this.googleOptions = googleOptions;
			systemInfo = new SystemInfo()
			{
				Version = versionService.Get(),
				Name = config.GetValue<string>("SystemInfo:Name"),
				AdminEmail = OptionsClient.GetData(config.GetValue<string>("SystemInfo:AdminEmail"))
			};
		}

		public Result<Dictionary<string, string>> GetSettings()
		{
			var settings = new Dictionary<string, string>()
			{
				{ "name", systemInfo.Name },
				{ "version", systemInfo.Version }
			};

			return Result.Ok(settings);
		}

		public async Task<Result<bool>> SendFeedback(Feedback dto)
		{
			return await ValidateDto(dto)
				.Bind(async () => await RecaptchaVerify(dto.Token))
				.Bind(SendMessage)
				.OnFailure(LogError);

			Result ValidateDto(Feedback dto)
			{
				var (_, isFailure, error) = Validate();
				return isFailure
					? Result.Failure(error)
					: Result.Ok();

				Result Validate() => GenericValidator<Feedback>.Validate(v =>
				{
					v.RuleFor(c => c.Name).NotEmpty();
					v.RuleFor(c => c.Email).EmailAddress().NotEmpty();
					v.RuleFor(c => c.Message).NotEmpty();
					v.RuleFor(c => c.Token).NotEmpty();
				}, dto);
			}

			async Task<Result> RecaptchaVerify(string token) => await AuthenticationHelper.RecaptchaTokenVerify(clientFactory, googleOptions, token);

			async Task<Result<bool>> SendMessage()
			{
				var (_, isFailure, error) = await mailSender.Send(EmailTemplateKeys.Feedback, systemInfo.AdminEmail, dto);
				return isFailure
					? Result.Failure<bool>($"An error occurred while sending feedback.")
					: Result.Ok(true);
			}
		}
	}
}
