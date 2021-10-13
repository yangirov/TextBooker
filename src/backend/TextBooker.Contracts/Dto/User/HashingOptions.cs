namespace TextBooker.Contracts.Dto.User
{
	public record HashingOptions
	{
		public int Iterations { get; set; } = 10000;
	}
}
