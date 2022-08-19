namespace TextBooker.Contracts.Dto
{
	public record SystemInfo
	{
		public string Version { get; set; }

		public string Name { get; set; }

		public string AdminEmail { get; set; }

		public string FrontendAppUrl { get; set; }
	}
}
