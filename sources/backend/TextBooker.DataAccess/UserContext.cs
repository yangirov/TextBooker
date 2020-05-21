using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TextBooker.DataAccess
{
	public class UserContext : IdentityDbContext
	{
		public UserContext(DbContextOptions<UserContext> options)
			: base(options)
		{
		}
	}
}
