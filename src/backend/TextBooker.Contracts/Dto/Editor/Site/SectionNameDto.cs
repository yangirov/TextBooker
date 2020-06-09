using System.ComponentModel.DataAnnotations;

namespace TextBooker.Contracts.Dto
{
	public class SectionNameDto
	{
		public string Id { get; set; }

		public string Content { get; set; }

		[Required]
		public int TemplateKeyId { get; set; }

		[Required]
		public string SiteId { get; set; }
	}
}
