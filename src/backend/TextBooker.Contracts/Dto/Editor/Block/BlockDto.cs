using System.ComponentModel.DataAnnotations;

namespace TextBooker.Contracts.Dto
{
	public class BlockDto
	{
		public string Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Alias { get; set; }

		public string Content { get; set; }

		[Required]
		public string SiteId { get; set; }
	}
}
