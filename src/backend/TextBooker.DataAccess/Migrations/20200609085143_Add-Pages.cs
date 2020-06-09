using Microsoft.EntityFrameworkCore.Migrations;

namespace TextBooker.DataAccess.Migrations
{
	public partial class AddPages : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "pages",
				columns: table => new
				{
					id = table.Column<string>(nullable: false),
					title = table.Column<string>(nullable: true),
					content = table.Column<string>(nullable: true),
					site_id = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_pages", x => x.id);
					table.ForeignKey(
						name: "fk_pages_sites_site_id",
						column: x => x.site_id,
						principalTable: "sites",
						principalColumn: "id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "ix_pages_site_id",
				table: "pages",
				column: "site_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "pages");
		}
	}
}
