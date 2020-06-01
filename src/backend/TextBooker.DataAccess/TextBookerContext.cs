using Microsoft.EntityFrameworkCore;
using TextBooker.DataAccess.Entities;

namespace TextBooker.DataAccess
{
	public class TextBookerContext : DbContext
	{
		public TextBookerContext(DbContextOptions<TextBookerContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }

		public DbSet<EmailTemplate> EmailTemplates { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<User>(user =>
			{
				user.HasKey(c => c.Id);
				user.HasIndex(b => b.Email).IsUnique();
			});

			builder.Entity<EmailTemplate>(emailTemplate =>
			{
				emailTemplate.HasKey(c => c.Id);
			});

			DataSeeder.AddData(builder);
		}
	}
}
