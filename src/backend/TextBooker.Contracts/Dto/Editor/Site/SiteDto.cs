using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TextBooker.Contracts.Dto
{
	public record SiteDto
	{
		public string Id { get; set; }

		public string UserId { get; set; }

		[Required]
		public string Title { get; set; }

		public string Description { get; set; }

		public string Keywords { get; set; }

		public string Icon { get; set; }

		public bool EnabledUserScripts { get; set; }

		public DateTime CreatedOn { get; set; } = DateTime.Now;

		public DateTime UpdatedOn { get; set; } = DateTime.Now;

		[Required]
		public int TemplateId { get; set; }

		public List<UserScriptDto> UserScripts { get; set; }

		public List<SectionNameDto> SectionNames { get; set; }
	}
}
