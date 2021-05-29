using System.Security.Claims;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TextBooker.Contracts.Dto.User;

namespace TextBooker.BusinessLogic.Services
{
	public interface IUserService
	{
		Task<Result<UserInfoDto>> GetInfo(ClaimsPrincipal user);

		Task<Result<bool>> Register(SignDto dto);

		Task<Result<SignResponse>> Login(SignDto dto);

		Task<Result<bool>> Update(ClaimsPrincipal user, UserUpdateDto dto);
	}
}
