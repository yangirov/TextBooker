using System.Reflection;

namespace TextBooker.BusinessLogic.Services
{
	public class VersionService : IVersionService
	{
		private const string UndefinedVersion = "undefined";

		public string Get()
		{
			var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

			return string.IsNullOrWhiteSpace(version)
				? UndefinedVersion
				: version;
		}
	}
}
