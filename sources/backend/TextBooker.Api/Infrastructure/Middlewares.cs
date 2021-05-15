using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using App.Metrics;
using App.Metrics.Counter;

namespace TextBooker.Api.Infrastructure
{
	public static class Middlewares
	{
		public static void AutoDiscoverRoutes(HttpContext context)
		{
			if (context.Request.Path.Value == "/favicon.ico")
				return;

			var keys = new List<string>();
			var vals = new List<string>();

			var routeData = context.GetRouteData();
			if (routeData != null)
			{
				keys.AddRange(routeData.Values.Keys);
				vals.AddRange(routeData.Values.Values.Select(p => p.ToString()));
			}

			keys.Add("method");
			vals.Add(context.Request.Method);
			keys.Add("response");
			vals.Add(context.Response.StatusCode.ToString());
			keys.Add("url");
			vals.Add(context.Request.Path.Value);

			Program.Metrics.Measure.Counter.Increment(new CounterOptions
			{
				Name = "api",
				MeasurementUnit = Unit.Calls,
				Tags = new MetricTags(keys.ToArray(), vals.ToArray())
			});
		}
	}
}
