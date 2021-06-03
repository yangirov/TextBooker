using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TextBooker.DataAccess.Entities
{
	public class User : IdentityUser, IEntity<string>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override string Id { get; set; }

		public ICollection<Site> Sites { get;}
	}
}
