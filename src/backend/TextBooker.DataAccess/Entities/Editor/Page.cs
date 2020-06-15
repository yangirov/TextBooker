namespace TextBooker.DataAccess.Entities
{
	public class Page : SiteContent, IEntity<string>
	{
		public string Description { get; set; }

		public string Keywords { get; set; }

		public bool InMainMenu { get; set; }

		public bool InAsideMenu { get; set; }
	}
}
