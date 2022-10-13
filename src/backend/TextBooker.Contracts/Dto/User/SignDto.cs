using System.ComponentModel.DataAnnotations;

namespace TextBooker.Contracts.Dto.User
{
	public record SignDto
	{
		public SignDto() { }

		public SignDto(string email, string password)
		{
			Email = email;
			Password = password;
		}

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		public string Token { get; set; }
	}
}
