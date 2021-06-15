using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TextBooker.Utils
{
	public static class OptionsClient
	{
		public static string GetData(string data)
		{
			return GetValue(data);
		}

		public static T GetData<T>(T data)
		{
			var props = data.GetType()
							.GetProperties(BindingFlags.Instance | BindingFlags.Public)
							.ToDictionary(p => p.Name, p => p.GetValue(data, null));

			var obj = PopulateObject(data, props);
			return obj;
		}

		private static T PopulateObject<T>(T obj, Dictionary<string, object> values)
		{
			foreach (var value in values)
			{
				var prop = obj.GetType().GetProperty(value.Key, BindingFlags.Instance | BindingFlags.Public);

				if (prop != null && prop.CanWrite)
					prop.SetValue(obj, GetValue(value.Value), null);
			}

			return obj;
		}

		private static T GetValue<T>(T value)
		{
			var envVariable = Environment.GetEnvironmentVariable(value.ToString());

			if (envVariable is null)
				return value;

			return (T)Convert.ChangeType(envVariable, typeof(T));
		}
	}
}
