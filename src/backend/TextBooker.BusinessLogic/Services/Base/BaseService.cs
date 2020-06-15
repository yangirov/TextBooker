using Serilog;

namespace TextBooker.BusinessLogic.Services
{
	public class BaseService
	{
		private readonly ILogger logger;

		public BaseService(ILogger logger)
        {
			this.logger = logger;
		}

		public void LogError(string error) => logger.Error(error);

		public void LogAudit(string message) => logger.Information(message);
	}
}
