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
		public DbSet<Page> Pages { get; set; }
		public DbSet<SiteFile> Files { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			BuildSite(builder);
			BuildIdentity(builder);
			BuildEmailTemplate(builder);
			BuildSiteTemplate(builder);

			DataSeeder.AddData(builder);
		}

		private void BuildSite(ModelBuilder builder)
		{
			builder.Entity<Site>()
				.HasMany(s => s.Pages)
				.WithOne(c => c.Site)
				.HasForeignKey(c => c.SiteId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Site>()
				.HasMany(s => s.Blocks)
				.WithOne(c => c.Site)
				.HasForeignKey(c => c.SiteId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Site>()
				.HasMany(s => s.Files)
				.WithOne(c => c.Site)
				.HasForeignKey(c => c.SiteId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Site>()
				.HasMany(s => s.UserScripts)
				.WithOne(c => c.Site)
				.HasForeignKey(c => c.SiteId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Site>()
				.HasMany(s => s.SectionNames)
				.WithOne(c => c.Site)
				.HasForeignKey(c => c.SiteId)
				.OnDelete(DeleteBehavior.Cascade);
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
