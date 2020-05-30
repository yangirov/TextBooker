using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TextBooker.Api.Infrastructure.Filters
{
	public class ModelValidationFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!filterContext.ModelState.IsValid)
				filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
		}

		public void OnActionExecuted(ActionExecutedContext context) { }
	}
}
