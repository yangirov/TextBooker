using System.Collections.Generic;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using TextBooker.Contracts.Dto;

namespace TextBooker.BusinessLogic.Services
{
	public interface ICommonService
	{
		Result<Dictionary<string, string>> GetSettings();

		Task<Result<bool>> SendFeedback(Feedback dto);
	}
}
