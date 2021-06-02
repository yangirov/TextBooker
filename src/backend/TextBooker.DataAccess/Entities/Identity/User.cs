using System;
using Microsoft.AspNetCore.Identity;

namespace TextBooker.DataAccess.Entities
{
	public class User : IdentityUser<Guid>, IEntity<Guid>
	{
	}
}
