using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TextBooker.BusinessLogic.Services;

namespace TextBooker.Api.Controllers
{
	[ApiController]
	[Route("settings")]
	[Produces("application/json")]
	public class SettingsController : ControllerBase
	{
		private readonly IConfiguration config;
		private readonly IVersionService versionService;

		public SettingsController(IConfiguration config, IVersionService versionService)
		{
			this.config = config;
			this.versionService = versionService;
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Get()
		{
			var settings = new Dictionary<string, string>
			{
				{ "version", versionService.Get() },
				{ "name", config.GetValue<string>("SystemInfo:Name") }
			};

			return Ok(settings);
		}
	}
}
