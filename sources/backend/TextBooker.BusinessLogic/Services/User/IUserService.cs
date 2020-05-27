using System.Security.Claims;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TextBooker.Contracts.Dto.User;

namespace TextBooker.BusinessLogic.Services
{
	public interface IUserService
	{
		Task<Result<UserShortInfoDto>> UserInfo(ClaimsPrincipal user);

		Task<Result<string>> Register(SignDto dto);

		Task<Result<SignResponse>> Login(SignDto dto);
	}
}
