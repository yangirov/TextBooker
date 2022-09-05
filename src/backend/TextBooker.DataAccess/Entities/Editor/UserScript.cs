using System.ComponentModel.DataAnnotations.Schema;

namespace TextBooker.DataAccess.Entities
{
	public class UserScript : IEntity<string>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		public int Location { get; set; }

		public string Content { get; set; }

		public string SiteId { get; set; }

		public Site Site { get; set; }
	}
}
