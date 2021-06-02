namespace TextBooker.DataAccess.Entities
{
	public class EmailTemplate : IEntity<int>
	{
		public int Id { get; set; }

		public string Subject { get; set; }

		public string Body { get; set; }

		public bool Importance { get; set; }
	}
}
