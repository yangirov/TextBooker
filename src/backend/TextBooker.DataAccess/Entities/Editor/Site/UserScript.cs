using System;

namespace TextBooker.DataAccess.Entities
{
	public class UserScript : IEntity<Guid>
	{
		public Guid Id { get; set; }
		
		public int Location { get; set; }

		public string Content { get; set; }

		public Guid SiteId { get; set; }
		public Site Site { get; set; }
	}
}
