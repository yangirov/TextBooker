using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using TextBooker.Contracts.Dto;
using TextBooker.Contracts.Enums;

namespace TextBooker.BusinessLogic.Services
{
	public class CommonService : ICommonService
	{
		private readonly IMailSender mailSender;
		private readonly SystemInfo systemInfo;

		public CommonService(IMailSender mailSender, IVersionService versionService, IConfiguration config) 
		{
			this.mailSender = mailSender;

			systemInfo = new SystemInfo()
			{
				Version = versionService.Get(),
				Name = config.GetValue<string>("SystemInfo:Name"),
				AdminEmail = Environment.GetEnvironmentVariable(config.GetValue<string>("SystemInfo:AdminEmail"))
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
			var result = await mailSender.Send(EmailTemplateKeys.Feedback, systemInfo.AdminEmail, dto);
			return result.IsFailure
				? Result.Failure<bool>($"An error occurred while sending feedback.")
				: Result.Ok(true);
		}
	}
}
