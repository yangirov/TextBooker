using AutoMapper;

using TextBooker.Contracts.Dto;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Mappers
{
	public class SiteProfile : Profile
	{
		public SiteProfile()
		{
			CreateMap<UserScript, UserScriptDto>().ReverseMap();

			CreateMap<SectionName, SectionNameDto>().ReverseMap();

			CreateMap<Site, SiteDto>().ReverseMap();

			CreateMap<Site, ProjectDto>().ReverseMap();

			CreateMap<Template, TemplateDto>().ReverseMap();

			CreateMap<TemplateKey, TemplateKeyDto>().ReverseMap();

			CreateMap<Block, BlockDto>().ReverseMap();

			CreateMap<Page, PageDto>().ReverseMap();
		}
	}
}
