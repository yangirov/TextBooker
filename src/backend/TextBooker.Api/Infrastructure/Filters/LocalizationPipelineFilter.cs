using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace TextBooker.Api.Infrastructure.Filters
{
	public class LocalizationPipelineFilter
	{
		public void Configure(IApplicationBuilder app, IOptions<RequestLocalizationOptions> options)
		{
			app.UseRequestLocalization(options.Value);
		}
	}
}
