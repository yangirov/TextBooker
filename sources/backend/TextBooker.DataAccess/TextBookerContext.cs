using Microsoft.EntityFrameworkCore;
using TextBooker.DataAccess.Entities;

namespace TextBooker.DataAccess
{
    public class TextBookerContext : DbContext
	{
		public TextBookerContext(DbContextOptions<TextBookerContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<User> Users { get; set; }
	}
}
