using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using FluentValidation;

using Microsoft.Extensions.Configuration;

using Serilog;

using TextBooker.BusinessLogic.Infrastructure;
using TextBooker.Common.Config;
using TextBooker.Contracts.Dto;
using TextBooker.Contracts.Enums;
using TextBooker.Utils;

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
			IMailSender mailSender,
			IVersionService versionService,
			IConfiguration config,
			IHttpClientFactory clientFactory,
			GoogleSettings googleOptions
		) : base(logger)
        {
			this.mailSender = mailSender;
			this.clientFactory = clientFactory;
			this.googleOptions = googleOptions;
			systemInfo = new SystemInfo()
			{
				Version = versionService.Get(),
				Name = config.GetValue<string>("SystemInfo:Name"),
				AdminEmail = VaultClient.GetData(config.GetValue<string>("SystemInfo:AdminEmail"))
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
			return await RecaptchaVerify(dto)
				.Bind(SendMessage)
				.OnFailure(LogError);

			async Task<Result> RecaptchaVerify(Feedback dto)
				=> await AuthenticationHelper.RecaptchaTokenVerify(clientFactory, googleOptions, dto.Token);

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
