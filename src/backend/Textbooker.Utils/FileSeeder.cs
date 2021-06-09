using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace TextBooker.Utils
{
	public static class FileSeeder
	{
		public static List<T> FromJson<T>(string fileName, string folderName = "Seed")
		{
			var filePath = @$"{folderName}{Path.DirectorySeparatorChar}{fileName}.json";

			if (!File.Exists(filePath))
				return new List<T>();

			return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath));
		}
	}
}
