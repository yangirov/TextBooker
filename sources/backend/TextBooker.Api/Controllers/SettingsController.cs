using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextBooker.BusinessLogic.Services;

namespace TextBooker.Api.Controllers
{
	[ApiController]
	[Route("settings")]
	[Produces("application/json")]
	public class SettingsController : ControllerBase
	{
		private readonly IVersionService versionService;

		public SettingsController(IVersionService versionService)
		{
			this.versionService = versionService;
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Get()
		{
			var settings = new Dictionary<string, string>
			{
				{ "version", versionService.Get() }
			};

			return Ok(settings);
		}
	}
}
