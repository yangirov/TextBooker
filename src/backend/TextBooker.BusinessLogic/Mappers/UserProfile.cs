using AutoMapper;

using TextBooker.Contracts.Dto.User;
using TextBooker.DataAccess.Entities;

namespace TextBooker.BusinessLogic.Mappers
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserInfoDto>();
		}
	}
}
