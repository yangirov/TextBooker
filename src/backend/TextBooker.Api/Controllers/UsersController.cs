using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TextBooker.Contracts.Dto.User;
using TextBooker.BusinessLogic.Services;

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

		// POST /user/register
		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] SignDto dto) => OkOrBadRequest(await userService.Register(dto));

		// POST /user/login
		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] SignDto dto) => OkOrBadRequest(await userService.Login(dto));

		// GET /user/confirm
		[AllowAnonymous]
		[HttpGet("confirm")]
		public async Task<IActionResult> ConfirmEmail(string email, string token) => OkOrBadRequest(await userService.ConfirmEmail(email, token));

		// GET /user/is-auth
		[AllowAnonymous]
		[HttpGet("is-auth")]
		public IActionResult IsAuth() => Ok(User.Identity.IsAuthenticated);

		// GET /user/info
		[HttpGet("info")]
		public async Task<IActionResult> UserInfo() => OkOrBadRequest(await userService.GetInfo(UserId));

		// PUT /user
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UserUpdateDto dto) => OkOrBadRequest(await userService.Update(UserId, dto));

		// DELETE /user
		[HttpDelete]
		public async Task<IActionResult> Delete() => OkOrBadRequest(await userService.Delete(UserId));
	}
}
