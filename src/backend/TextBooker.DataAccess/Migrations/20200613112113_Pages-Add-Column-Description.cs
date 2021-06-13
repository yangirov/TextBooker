using Microsoft.EntityFrameworkCore.Migrations;

namespace TextBooker.DataAccess.Migrations
{
    public partial class PagesAddColumnDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keywords",
                table: "pages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "pages");

            migrationBuilder.DropColumn(
                name: "keywords",
                table: "pages");
        }
    }
}
