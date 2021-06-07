using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TextBooker.Contracts.Dto;

namespace TextBooker.BusinessLogic.Services
{
	public interface IEditorService
	{
		Task<Result<SiteCreateDto>> Create(SiteCreateDto dto);

		Task<Result<List<SiteListItemDto>>> GetUserSites(string userId);

		Task<Result<List<TemplateDto>>> GetTemplates();

		Task<Result<List<TemplateKeyDto>>> GetTemplateKeys();
	}
}
