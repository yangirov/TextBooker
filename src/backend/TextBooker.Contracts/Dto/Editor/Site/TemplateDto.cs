using System;

namespace TextBooker.Contracts.Dto
{
	public record TemplateDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Screenshot { get; set; }

		public string Author { get; set; }

		public Uri AuthorUrl { get; set; }

		public string License { get; set; }

		public string About { get; set; }
	}
}
