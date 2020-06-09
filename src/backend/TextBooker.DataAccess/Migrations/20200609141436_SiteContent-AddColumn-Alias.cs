using Microsoft.EntityFrameworkCore.Migrations;

namespace TextBooker.DataAccess.Migrations
{
    public partial class SiteContentAddColumnAlias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "alias",
                table: "pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "alias",
                table: "blocks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alias",
                table: "pages");

            migrationBuilder.DropColumn(
                name: "alias",
                table: "blocks");
        }
    }
}
