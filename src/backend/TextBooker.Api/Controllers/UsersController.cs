using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TextBooker.BusinessLogic.Services;
using TextBooker.Contracts.Dto.User;

namespace TextBooker.Api.Controllers
{
	[ApiController]
	[Route("user")]
	[Produces("application/json")]
	public class UserController : BaseController
	{
		private readonly IUserService userService;

		public UserController(IUserService userService)
		{
			this.userService = userService;
		}

		/// <summary>
		/// Register user
		/// </summary>
		/// <param name="dto">Sign data</param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] SignDto dto) => OkOrBadRequest(await userService.Register(dto));

		/// <summary>
		/// Login user
		/// </summary>
		/// <param name="dto">Sign data</param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] SignDto dto) => OkOrBadRequest(await userService.Login(dto));

		/// <summary>
		/// Confirm user email
		/// </summary>
		/// <param name="email">Email</param>
		/// <param name="token">Authorization token</param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpGet("confirm")]
		public async Task<IActionResult> ConfirmEmail(string email, string token) => OkOrBadRequest(await userService.ConfirmEmail(email, token));

		/// <summary>
		/// Get user authorization status
		/// </summary>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpGet("is-auth")]
		public IActionResult IsAuth() => Ok(User.Identity.IsAuthenticated);

		/// <summary>
		/// Get user info
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpGet("info")]
		public async Task<IActionResult> UserInfo() => OkOrBadRequest(await userService.GetInfo(UserId));

		/// <summary>
		/// Update user info
		/// </summary>
		/// <param name="dto">User data</param>
		/// <returns></returns>
		[Authorize]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UserUpdateDto dto) => OkOrBadRequest(await userService.Update(UserId, dto));

		/// <summary>
		/// Delete user
		/// </summary>
		/// <returns></returns>
		[Authorize]
		[HttpDelete]
		public async Task<IActionResult> Delete() => OkOrBadRequest(await userService.Delete(UserId));
	}
}
