using System.ComponentModel.DataAnnotations;

namespace TextBooker.Contracts.Dto
{
	public class Feedback
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Message { get; set; }

		[Required]
		public string Token { get; set; }
	}
}
