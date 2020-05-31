using System.Collections.Generic;

namespace TextBooker.Contracts.Dto.Email
{
	public class EmailMessage
	{
		public string From { get; set; }

		public IEnumerable<string> To { get; set; }

		public string Subject { get; set; }

		public string Body { get; set; }

		public bool Importance { get; set; }
	}
}
