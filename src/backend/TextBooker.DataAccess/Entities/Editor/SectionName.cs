using System.ComponentModel.DataAnnotations.Schema;

namespace TextBooker.DataAccess.Entities
{
	public class SectionName : IEntity<string>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		public string Content { get; set; }

		public int TemplateKeyId { get; set; }

		public string SiteId { get; set; }

		public Site Site { get; set; }
	}
}
