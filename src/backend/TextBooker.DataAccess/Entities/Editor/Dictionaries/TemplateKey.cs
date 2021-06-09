namespace TextBooker.DataAccess.Entities
{
	public class TemplateKey : IEntity<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}
}
