using Microsoft.EntityFrameworkCore;
using TextBooker.DataAccess.Entities;

namespace TextBooker.DataAccess
{
	public class TextBookerContext : DbContext
	{
		public TextBookerContext(DbContextOptions<TextBookerContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<EmailTemplate> EmailTemplates { get; set; }
		public DbSet<Site> Sites { get; set; }
		public DbSet<Template> Templates { get; set; }
		public DbSet<TemplateKey> TemplateKeys { get; set; }
		public DbSet<UserScript> UserScripts { get; set; }
		public DbSet<SectionName> SectionNames { get; set; }

		public DbSet<Block> Blocks { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			BuildIdentity(builder);
			BuildEmailTemplate(builder);
			BuildSiteTemplate(builder);

			DataSeeder.AddData(builder);
		}

		private void BuildIdentity(ModelBuilder builder)
		{
			builder.Entity<User>(user =>
			{
				user.HasKey(c => c.Id);
				user.HasIndex(b => b.Email).IsUnique();
			});
		}

		private void BuildEmailTemplate(ModelBuilder builder)
		{
			builder.Entity<EmailTemplate>(emailTemplate =>
			{
				emailTemplate.HasKey(c => c.Id);
			});
		}

		private void BuildSiteTemplate(ModelBuilder builder)
		{
			builder.Entity<Template>()
				.HasIndex(t => t.Name).IsUnique();
		}
	}
}
