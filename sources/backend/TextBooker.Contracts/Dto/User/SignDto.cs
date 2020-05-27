using System.ComponentModel.DataAnnotations;

namespace TextBooker.Contracts.Dto.User
{
	public class SignDto
	{
		public SignDto() { }

		public SignDto(string email, string password)
		{
			Email = email;
			Password = password;
		}

		[Required]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
