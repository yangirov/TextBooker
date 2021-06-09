using System.ComponentModel.DataAnnotations;

using TextBooker.Contracts.Enums;

namespace TextBooker.Contracts.Dto
{
	public class UserScriptDto
	{
		public string Id { get; set; }

		[Required]
		public string SiteId { get; set; }

		[Required]
		public UserScriptLocation Location { get; set; }

		public string Content { get; set; }
	}
}
