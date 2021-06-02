using AutoMapper;
using TextBooker.Contracts.Dto;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Mappers
{
	public class SiteProfile : Profile
	{
		public SiteProfile()
		{
			CreateMap<UserScript, UserScriptDto>();
			CreateMap<UserScriptDto, UserScript>();

			CreateMap<SectionName, SectionNameDto>();
			CreateMap<SectionNameDto, SectionName>();

			CreateMap<Site, SiteCreateDto>();
			CreateMap<SiteCreateDto, Site>();

			CreateMap<Site, SiteListItemDto>();
		}
	}
}
