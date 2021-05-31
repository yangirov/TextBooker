using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TextBooker.Contracts.Enums;

namespace TextBooker.BusinessLogic.Services
{
    public interface IMailSender
    {
		Task<Result> Send<T>(EmailTemplateKeys template, string recipientAddress, T messageData);

		Task<Result> Send<T>(EmailTemplateKeys template, IEnumerable<string> recipientAddresses, T messageData);
	}
}
