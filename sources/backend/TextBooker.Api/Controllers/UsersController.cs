using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TextBooker.Contracts.Dto.User;
using TextBooker.BusinessLogic.Services;

namespace TextBooker.Api.Controllers
{
	[Route("user")]
	public class UserController : BaseController
	{
		private readonly IUserService userService;

		public UserController(IUserService userService)
		{
			this.userService = userService;
		}

		// GET user/is-auth
		[AllowAnonymous]
		[HttpGet("is-auth")]
		public IActionResult IsAuth() => Ok(User.Identity.IsAuthenticated);

		// GET user/info
		[AllowAnonymous]
		[HttpGet("info")]
		public async Task<IActionResult> UserInfo() => OkOrBadRequest(await userService.UserInfo(User));

		// POST user/register
		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] SignDto dto) => OkOrBadRequest(await userService.Register(dto));

		// POST user/login
		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] SignDto dto) => OkOrBadRequest(await userService.Login(dto));
	}
}
