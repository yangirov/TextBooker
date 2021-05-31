using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TextBooker.Contracts.Dto.User;

namespace TextBooker.BusinessLogic.Services
{
	public interface IUserService
	{
		Task<Result<UserInfoDto>> GetInfo(string userId);

		Task<Result<bool>> Register(SignDto dto);

		Task<Result<SignResponse>> Login(SignDto dto);

		Task<Result<bool>> ConfirmEmail(string email, string token);

		Task<Result<bool>> Update(string userId, UserUpdateDto dto);

		Task<Result<bool>> Delete(string userId);
	}
}
