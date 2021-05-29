using System;

namespace TextBooker.Api.Infrastructure
{
	public static class EnvironmentVariable
	{
		public static string Get(string key) => Environment.GetEnvironmentVariable(key) ?? null;
	}
}
