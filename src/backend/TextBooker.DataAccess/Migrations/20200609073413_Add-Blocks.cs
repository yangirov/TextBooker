using Microsoft.EntityFrameworkCore.Migrations;

namespace TextBooker.DataAccess.Migrations
{
	public partial class AddBlocks : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "blocks",
				columns: table => new
				{
					id = table.Column<string>(nullable: false),
					title = table.Column<string>(nullable: true),
					content = table.Column<string>(nullable: true),
					site_id = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_blocks", x => x.id);
					table.ForeignKey(
						name: "fk_blocks_sites_site_id",
						column: x => x.site_id,
						principalTable: "sites",
						principalColumn: "id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "ix_blocks_site_id",
				table: "blocks",
				column: "site_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "blocks");
		}
	}
}
