using AutoMapper;

using TextBooker.Contracts.Dto;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Mappers
{
	public class EditorProfile : Profile
	{
		public EditorProfile()
		{
			CreateMap<Template, TemplateDto>().ReverseMap();

			CreateMap<TemplateKey, TemplateKeyDto>().ReverseMap();

			CreateMap<Site, SiteDto>().ReverseMap();

			CreateMap<Site, ProjectDto>().ReverseMap();

			CreateMap<UserScript, UserScriptDto>().ReverseMap();

			CreateMap<SectionName, SectionNameDto>().ReverseMap();

			CreateMap<Block, BlockDto>().ReverseMap();

			CreateMap<Page, PageDto>().ReverseMap();
		}
	}
}
