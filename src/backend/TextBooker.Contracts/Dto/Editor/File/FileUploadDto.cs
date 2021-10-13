using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

using TextBooker.Contracts.Enums;

namespace TextBooker.Contracts.Dto
{
	public record FileUploadDto
	{
		[Required]
		public IFormFile File { get; set; }

		[Required]
		public FileType Type { get; set; }

		[Required]
		public string SiteId { get; set; }

		public string UserId { get; set; }

		public string Directory { get; set; }

		public string FileName { get; set; }

		public string FilePath { get; set; }

		public long Length { get; set; }

		public string Hash { get; set; }

		public DateTime UploadedOn { get; set; } = DateTime.Now;
	}
}
