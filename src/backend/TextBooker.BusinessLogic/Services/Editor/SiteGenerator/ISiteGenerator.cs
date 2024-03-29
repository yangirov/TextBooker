using System.Threading.Tasks;

using CSharpFunctionalExtensions;

namespace TextBooker.BusinessLogic.Services
{
	public interface ISiteGenerator
	{
		Task<Result<bool>> Generate(string siteId, string userId);

		Result ClearSiteFolder(string sitePath);

		string GetSitePath(string siteId);
	}
}
