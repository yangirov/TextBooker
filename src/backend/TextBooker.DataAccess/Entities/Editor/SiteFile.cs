using System.ComponentModel.DataAnnotations.Schema;

namespace TextBooker.DataAccess.Entities
{
	public class SiteFile : IEntity<string>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		public int Type { get; set; }

		public string FileName { get; set; }

		public string FilePath { get; set; }

		public string Hash { get; set; }

		public long Length { get; set; }

		public string UserId { get; set; }
		public User User { get; set; }

		public string SiteId { get; set; }
		public Site Site { get; set; }
	}
}
