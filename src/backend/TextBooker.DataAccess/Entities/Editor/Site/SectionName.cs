using System;

namespace TextBooker.DataAccess.Entities
{
	public class SectionName : IEntity<Guid>
	{
		public Guid Id { get; set; }

		public string Content { get; set; }

		public int TemplateKeyId { get; set; }
		public TemplateKey TemplateKey { get; set; }

		public Guid SiteId { get; set; }
		public Site Site { get; set; }
	}
}
