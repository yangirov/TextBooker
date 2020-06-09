using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Serilog.Sinks.Loki.Labels;

namespace TextBooker.Api.Infrastructure
{
	public class LogLabelProvider : ILogLabelProvider
	{
		private readonly IConfiguration configuraion;
		private readonly IWebHostEnvironment env;

		public LogLabelProvider(IConfiguration configuraion, IWebHostEnvironment env)
		{
			this.configuraion = configuraion;
			this.env = env;
		}

		public IList<LokiLabel> GetLabels()
			=> new List<LokiLabel>
			{
				new LokiLabel("app", configuraion.GetValue<string>("SystemInfo:Name")),
				new LokiLabel("environment", env.EnvironmentName)
			};
	}
}
