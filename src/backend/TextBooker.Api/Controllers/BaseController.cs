using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;
using TextBooker.Api.Infrastructure;

namespace TextBooker.Api.Controllers
{
	public class BaseController : ControllerBase
	{
		protected IActionResult OkOrBadRequest<T>(Result<T> model)
		{
			var (_, isFailure, response, error) = model;
			if (isFailure)
				return BadRequest(ProblemDetailsBuilder.Build(error));

			return Ok(response);
		}

		protected async Task<IActionResult> OkOrBadRequest<T>(Task<Result<T>> task) => OkOrBadRequest(await task);
	}
}
