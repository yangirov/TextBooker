namespace TextBooker.Contracts.Dto.User
{
	public class SignResponse
	{
		public SignResponse(string token, string email)
		{
			Token = token;
			Email = email;
		}

		public string Token { get; private set; }

		public string Email { get; private set; }
	}
}
