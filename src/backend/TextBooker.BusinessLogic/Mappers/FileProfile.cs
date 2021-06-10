using AutoMapper;

using TextBooker.Contracts.Dto;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Mappers
{
	public class FileProfile : Profile
	{
		public FileProfile()
		{
			CreateMap<SiteFile, FileUploadDto>().ReverseMap();
		}
	}
}
