using System;

namespace TextBooker.DataAccess.Entities
{
	public class Template : IEntity<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Screenshot { get; set; }

		public string Author { get; set; }

		public Uri AuthorUrl { get; set; }

		public string License { get; set; }

		public string About { get; set; }

		public string BlockBegin { get; set; }

		public string BlockEnd { get; set; }

		public string BlockTitleBegin { get; set; }

		public string BlockTitleEnd { get; set; }

		public string BlockContentBegin { get; set; }

		public string BlockContentEnd { get; set; }
	}
}
