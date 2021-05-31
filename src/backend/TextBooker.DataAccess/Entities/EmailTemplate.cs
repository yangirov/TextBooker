using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextBooker.DataAccess.Entities
{
	public class EmailTemplate : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Subject { get; set; }

		public string Body { get; set; }

		public bool Importance { get; set; }
	}
}
