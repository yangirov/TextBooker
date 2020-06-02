using System;
using System.Collections.Generic;

namespace TextBooker.DataAccess.Entities
{
	public class Site : IEntity<Guid>
	{
		public Guid Id { get; set; }

		public string UserId { get; set; }

		public User User { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Keywords { get; set; }

		public bool EnabledUserScripts { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime UpdatedOn { get; set; }

		public int TemplateId { get; set; }
		public Template Template { get; set; }

		public ICollection<UserScript> UserScripts { get; set; }

		public ICollection<SectionName> SiteSectionKeys { get; set; }
	}
}
