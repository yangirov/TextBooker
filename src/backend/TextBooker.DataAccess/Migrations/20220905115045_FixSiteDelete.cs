using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextBooker.DataAccess.Migrations
{
    public partial class FixSiteDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_blocks_sites_site_id",
                table: "blocks");

            migrationBuilder.DropForeignKey(
                name: "fk_files_sites_site_id",
                table: "files");

            migrationBuilder.DropForeignKey(
                name: "fk_pages_sites_site_id",
                table: "pages");

            migrationBuilder.DropForeignKey(
                name: "fk_section_names_sites_site_id",
                table: "section_names");

            migrationBuilder.DropForeignKey(
                name: "fk_user_scripts_sites_site_id",
                table: "user_scripts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_on",
                table: "sites",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "sites",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "fk_blocks_sites_site_id",
                table: "blocks",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_files_sites_site_id",
                table: "files",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_pages_sites_site_id",
                table: "pages",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_section_names_sites_site_id",
                table: "section_names",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_scripts_sites_site_id",
                table: "user_scripts",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_blocks_sites_site_id",
                table: "blocks");

            migrationBuilder.DropForeignKey(
                name: "fk_files_sites_site_id",
                table: "files");

            migrationBuilder.DropForeignKey(
                name: "fk_pages_sites_site_id",
                table: "pages");

            migrationBuilder.DropForeignKey(
                name: "fk_section_names_sites_site_id",
                table: "section_names");

            migrationBuilder.DropForeignKey(
                name: "fk_user_scripts_sites_site_id",
                table: "user_scripts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_on",
                table: "sites",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_on",
                table: "sites",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "fk_blocks_sites_site_id",
                table: "blocks",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_files_sites_site_id",
                table: "files",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_pages_sites_site_id",
                table: "pages",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_section_names_sites_site_id",
                table: "section_names",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_scripts_sites_site_id",
                table: "user_scripts",
                column: "site_id",
                principalTable: "sites",
                principalColumn: "id");
        }
    }
}
