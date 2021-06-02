using TextBooker.Common.Enums;

namespace TextBooker.Contracts.Dto
{
	public class UserScriptDto
	{
		public string Id { get; set; }

		public string SiteId { get; set; }

		public UserScriptLocation Location { get; set; }

		public string Content { get; set; }
	}
}
