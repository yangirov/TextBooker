using System.IO;

namespace TextBooker.Common
{
	public static class StringUtils
	{
		private static string sep = Path.DirectorySeparatorChar.ToString();

		public static string GetFilePath(string siteId, string filename) => Path.Combine("sites", siteId, "assets", filename);

		public static string ConvertToUrl(string path) => $"/{path}".Replace(sep, "/");
	}
}
