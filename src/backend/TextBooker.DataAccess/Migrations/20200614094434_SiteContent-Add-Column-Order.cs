using Microsoft.EntityFrameworkCore.Migrations;

namespace TextBooker.DataAccess.Migrations
{
    public partial class SiteContentAddColumnOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "pages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "blocks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order",
                table: "pages");

            migrationBuilder.DropColumn(
                name: "order",
                table: "blocks");
        }
    }
}
