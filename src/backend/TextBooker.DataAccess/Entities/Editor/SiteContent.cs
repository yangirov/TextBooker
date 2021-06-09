using System.ComponentModel.DataAnnotations.Schema;

namespace TextBooker.DataAccess.Entities
{
	public class SiteContent
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		public string Title { get; set; }

		public string Alias { get; set; }

		public string Content { get; set; }

		public string SiteId { get; set; }
		public Site Site { get; set; }
	}
}
