namespace TextBooker.Contracts.Dto.User
{
	public record SignResponse
	{
		public SignResponse(string token, string email, string host)
		{
			Token = token;
			Email = email;
			Host = host;
		}

		public string Token { get; private set; }

		public string Email { get; private set; }

		public string Host { get; set; }
	}
}
