using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TextBooker.Api.Controllers
{
	[ApiController]
	[Route("user")]
	[Produces("application/json")]
	public class UsersController : BaseController
	{
		[AllowAnonymous]
		[HttpGet("is-auth")]
		public IActionResult IsAuth()
		{
			return Ok(true);
		}
	}
}
