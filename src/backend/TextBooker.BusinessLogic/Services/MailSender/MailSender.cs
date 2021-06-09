using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.EntityFrameworkCore;

using MimeKit;

using Serilog;

using TextBooker.Common.Config;
using TextBooker.Contracts.Enums;
using TextBooker.DataAccess;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Services
{
	public class MailSender : BaseService, IMailSender
	{
		private readonly ILogger logger;
		private readonly TextBookerContext db;
		private readonly EmailSettings emailSettings;

		public MailSender(ILogger logger, TextBookerContext db, EmailSettings emailSettings) : base(logger, db)
		{
			this.logger = logger;
			this.db = db;
			this.emailSettings = emailSettings;
		}

		public async Task<Result> Send<T>(EmailTemplateKeys templateId, string recipientAddress, T messageData)
			=> await Send(templateId, new[] { recipientAddress }, messageData);

		public async Task<Result> Send<T>(EmailTemplateKeys templateId, IEnumerable<string> recipientAddresses, T messageData)
		{
			var mailingList = recipientAddresses as string[] ?? recipientAddresses.ToArray();
			if (!mailingList.Any())
				return Result.Failure("No recipient addresses provided");

			var template = await FindTemplate(templateId);
			var templateData = GetTemplateData(messageData);

			return await Result.Combine(template, templateData)
				.Bind(() => BuildMessage(template.Value, templateData.Value))
				.Bind(async (message) => await SendMessage(template.Value, message, mailingList))
				.OnFailure(LogError);
		}

		private async Task<Result<EmailTemplate>> FindTemplate(EmailTemplateKeys template)
		{
			var templateId = (int)template;
			var templateEntity = await db.EmailTemplates.SingleOrDefaultAsync(t => t.Id == templateId) ?? Maybe<EmailTemplate>.None;
			return templateEntity.HasNoValue
				? Result.Failure<EmailTemplate>($"The template with this identifier was not found: {(int)template}")
				: Result.Ok(templateEntity.Value);
		}

		private Result<Dictionary<string, object>> GetTemplateData<T>(T data)
			=> Result.Ok(data.GetType()
							 .GetProperties(BindingFlags.Instance | BindingFlags.Public)
							 .ToDictionary(prop => prop.Name.ToLower(), prop => prop.GetValue(data, null)));

		private Result<string> BuildMessage(EmailTemplate template, IDictionary<string, object> parameters)
			=> Result.Ok(parameters.Aggregate(template.Body, (acc, item) => acc.Replace($"%{item.Key}%", item.Value.ToString())));

		private async Task<Result> SendMessage(EmailTemplate template, string bodyMessage, IEnumerable<string> mailingList)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(emailSettings.Sender, emailSettings.Username));
			message.To.AddRange(mailingList.Select(m => new MailboxAddress(string.Empty, m)));
			message.Subject = template.Subject;
			message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = bodyMessage };
			message.Importance = template.Importance ? MessageImportance.High : MessageImportance.Normal;

			try
			{
				using (var smtpClient = new SmtpClient())
				{
					smtpClient.AuthenticationMechanisms.Clear();
					smtpClient.AuthenticationMechanisms.Add("NTLM");
					smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

					await smtpClient.ConnectAsync(emailSettings.Host, Convert.ToInt32(emailSettings.Port), SecureSocketOptions.Auto);
					await smtpClient.AuthenticateAsync(new NetworkCredential(emailSettings.Username, emailSettings.Password));
					await smtpClient.SendAsync(message);
					await smtpClient.DisconnectAsync(true);
				}

				logger.Information("The email was successfully sent", message);
				return Result.Ok();
			}
			catch (Exception e)
			{
				var error = $"An error occurred while sending the message: \"{message}\". Error: {e.Message}";
				return Result.Failure(error);
			}
		}
	}
}
