using Microsoft.EntityFrameworkCore.Migrations;

namespace TextBooker.DataAccess.Migrations
{
    public partial class PageAddColumnsMenuLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "in_aside_menu",
                table: "pages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "in_main_menu",
                table: "pages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 15,
                column: "about",
                value: "Red theme with a single sidebar and a very mouth-watering title");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 18,
                column: "about",
                value: "Nice theme in beige tones with a side column on the left.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 19,
                column: "about",
                value: "Light theme with a blue-green header and a sidebar on the left. Main menu in the sidebar.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 26,
                column: "about",
                value: "Strict gray theme with a side column on the left. The main menu in the sidebar.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 29,
                column: "about",
                value: "Very beautiful bright green and black theme with one side column.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 31,
                column: "about",
                value: "Bright creative light blue theme with a block \"On site\".");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "in_aside_menu",
                table: "pages");

            migrationBuilder.DropColumn(
                name: "in_main_menu",
                table: "pages");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 15,
                column: "about",
                value: "a Red theme with a single sidebar and a very mouth-watering title");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 18,
                column: "about",
                value: "a Nice theme in beige tones with a side column on the left.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 19,
                column: "about",
                value: "a Light theme with a blue-green header and a sidebar on the left. Main menu in the sidebar.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 26,
                column: "about",
                value: "a Strict gray theme with a side column on the left. The main menu in the sidebar.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 29,
                column: "about",
                value: "a Very beautiful bright green and black theme with one side column.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 31,
                column: "about",
                value: "a Bright creative light blue theme with a block \"About site\".");
        }
    }
}
