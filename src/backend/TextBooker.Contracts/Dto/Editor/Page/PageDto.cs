using System.ComponentModel.DataAnnotations;

namespace TextBooker.Contracts.Dto
{
	public class PageDto
	{
		public string Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Description { get; set; }

		public string Keywords { get; set; }

		[Required]
		public string Alias { get; set; }

		public string Content { get; set; }

		[Required]
		public string SiteId { get; set; }
	}
}
