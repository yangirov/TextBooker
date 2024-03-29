﻿using System.ComponentModel.DataAnnotations;

namespace TextBooker.Contracts.Dto
{
	public record BlockDto
	{
		public string Id { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Alias { get; set; }

		public string Content { get; set; }

		public int Order { get; set; }

		[Required]
		public string SiteId { get; set; }
	}
}
