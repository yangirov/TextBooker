using System.Collections.Generic;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using TextBooker.Contracts.Dto;

namespace TextBooker.BusinessLogic.Services
{
	public interface ISiteService
	{
		Task<Result<string>> Create(SiteDto dto);

		Task<Result<SiteDto>> Get(string siteId, string userId);

		Task<Result<SiteDto>> Update(SiteDto dto);

		Task<Result<bool>> Delete(string siteId, string userId);

		Task<Result<List<ProjectDto>>> GetProjects(string userId);

		Task<Result<List<TemplateDto>>> GetTemplates();

		Task<Result<List<TemplateKeyDto>>> GetTemplateKeys();
	}
}
