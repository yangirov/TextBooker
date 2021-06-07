using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TextBooker.Contracts.Dto;

namespace TextBooker.BusinessLogic.Services
{
	public interface IEditorService
	{
		Task<Result<SiteDto>> Create(SiteDto dto);

		Task<Result<SiteDto>> Get(string siteId, string userId);

		Task<Result<List<SiteListItemDto>>> GetUserSites(string userId);

		Task<Result<List<TemplateDto>>> GetTemplates();

		Task<Result<List<TemplateKeyDto>>> GetTemplateKeys();		
	}
}
