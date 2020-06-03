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

			CreateMap<Site, SiteCreateDto>().ReverseMap();

			CreateMap<Site, SiteListItemDto>().ReverseMap();
		}
	}
}
