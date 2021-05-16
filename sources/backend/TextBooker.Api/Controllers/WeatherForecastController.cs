using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using TextBooker.Contracts.Dto;

namespace TextBooker.Api.Controllers
{
	[ApiController]
	[Route("test")]
	[Produces("application/json")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot"
		};

		private readonly ILogger logger;

		public WeatherForecastController(ILogger logger)
		{
			this.logger = logger;
		}

		[HttpGet]
		public IActionResult Get()
		{
			logger.Information("Get test info");

			var rng = new Random();
			var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();

			return Ok(result);
		}
	}
}
