﻿using System;

namespace TextBooker.Contracts.Dto
{
	public record ProjectDto
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime UpdatedOn { get; set; }
	}
}
