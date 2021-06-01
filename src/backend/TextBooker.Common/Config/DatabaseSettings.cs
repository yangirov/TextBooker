namespace TextBooker.Common.Config
{
	public class DatabaseSettings
	{
		public string ConnectionString { get; set; }

		public int PoolSize { get; set; } = 16;
	}
}
