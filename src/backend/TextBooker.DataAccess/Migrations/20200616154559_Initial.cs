using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TextBooker.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "email_templates",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject = table.Column<string>(nullable: true),
                    body = table.Column<string>(nullable: true),
                    importance = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "template_keys",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_template_keys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    screenshot = table.Column<string>(nullable: true),
                    author = table.Column<string>(nullable: true),
                    author_url = table.Column<string>(nullable: true),
                    license = table.Column<string>(nullable: true),
                    about = table.Column<string>(nullable: true),
                    block_begin = table.Column<string>(nullable: true),
                    block_end = table.Column<string>(nullable: true),
                    block_title_begin = table.Column<string>(nullable: true),
                    block_title_end = table.Column<string>(nullable: true),
                    block_content_begin = table.Column<string>(nullable: true),
                    block_content_end = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_name = table.Column<string>(nullable: true),
                    normalized_user_name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    normalized_email = table.Column<string>(nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sites",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_id = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    keywords = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    enabled_user_scripts = table.Column<bool>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: false),
                    template_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sites", x => x.id);
                    table.ForeignKey(
                        name: "fk_sites_templates_template_id",
                        column: x => x.template_id,
                        principalTable: "templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sites_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "blocks",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    alias = table.Column<string>(nullable: true),
                    content = table.Column<string>(nullable: true),
                    site_id = table.Column<string>(nullable: true),
                    order = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    file_name = table.Column<string>(nullable: true),
                    file_path = table.Column<string>(nullable: true),
                    hash = table.Column<string>(nullable: true),
                    length = table.Column<long>(nullable: false),
                    user_id = table.Column<string>(nullable: true),
                    site_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.id);
                    table.ForeignKey(
                        name: "fk_files_sites_site_id",
                        column: x => x.site_id,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_files_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    alias = table.Column<string>(nullable: true),
                    content = table.Column<string>(nullable: true),
                    site_id = table.Column<string>(nullable: true),
                    order = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    keywords = table.Column<string>(nullable: true),
                    in_main_menu = table.Column<bool>(nullable: false),
                    in_aside_menu = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "section_names",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    content = table.Column<string>(nullable: true),
                    template_key_id = table.Column<int>(nullable: false),
                    site_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_section_names", x => x.id);
                    table.ForeignKey(
                        name: "fk_section_names_sites_site_id",
                        column: x => x.site_id,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_scripts",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    location = table.Column<int>(nullable: false),
                    content = table.Column<string>(nullable: true),
                    site_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_scripts", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_scripts_sites_site_id",
                        column: x => x.site_id,
                        principalTable: "sites",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "email_templates",
                columns: new[] { "id", "body", "importance", "subject" },
                values: new object[,]
                {
                    { 1, "Hi! Please follow <a href=\"%host%/#/user/email-confirm?email=%email%&token=%token%\">this link</a> to verify your email address.", false, "Invite to TextBooker" },
                    { 2, "Name: %name%<br> Email: %email%<br> Message: %message%", false, "Feedback from TextBooker" }
                });

            migrationBuilder.InsertData(
                table: "template_keys",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 4, "AboutSiteTitle" },
                    { 3, "PageListTitle" },
                    { 2, "Subtitle" },
                    { 1, "MainMenuTitle" },
                    { 5, "AboutSiteShort" },
                    { 6, "Footer" }
                });

            migrationBuilder.InsertData(
                table: "templates",
                columns: new[] { "id", "about", "author", "author_url", "block_begin", "block_content_begin", "block_content_end", "block_end", "block_title_begin", "block_title_end", "license", "name", "screenshot" },
                values: new object[,]
                {
                    { 7, "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Red)", "screenshot.jpg" },
                    { 1, "This is a standard theme with blocks and a single sidebar on the right. In the title of the starnitsa-drawing of the atom model. A great topic for an electronic physics textbook.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<div class=\"boxed\">", "<div class=\"content\">", "</div>", "</div>", "<h2 class=\"title\">", "</h2>", "Creative Commons Attribution 2.5", "Atomohost", "screenshot.jpg" },
                    { 2, "Simple and modern light theme.", "Switchroyale", "http://www.switchroyale.com/", "<li> ", "<div>", "</div>", "</li>", "<h2>", "</h2>", null, "Azul", "screenshot.jpg" },
                    { 3, "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Blue)", "screenshot.jpg" },
                    { 4, "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Green)", "screenshot.jpg" },
                    { 31, "Bright creative light blue theme with a block \"On site\".", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Snowglass", "screenshot.jpg" },
                    { 30, "Subject mountains, blue tone, main menu in sidebar.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Mountain Breeze", "screenshot.jpg" },
                    { 29, "Very beautiful bright green and black theme with one side column.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<ul><li><p>", "</p></li></ul>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Marked", "screenshot.jpg" },
                    { 28, "Grey-green theme with left-side column and the picture in the header on vegetable theme.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<ul><li><p>", "</p></li></ul>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Lily Pads", "screenshot.jpg" },
                    { 27, "Light orange theme with one sidebar column.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Leaf Brush", "screenshot.jpg" },
                    { 26, "Strict gray theme with a side column on the left. The main menu in the sidebar.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Indicator", "screenshot.jpg" },
                    { 25, "Light blue theme with one side column.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Indication", "screenshot.jpg" },
                    { 24, "Wood and metal.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Improved", "screenshot.jpg" },
                    { 23, "Light theme in brown tones with a single sidebar.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Guarantee", "screenshot.jpg" },
                    { 6, "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Pink)", "screenshot.jpg" },
                    { 22, "Beautiful orange-green theme with flowers in the title.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Fresh Scent", "screenshot.jpg" },
                    { 20, "Light blue theme with right side column.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Exploitable", "screenshot.jpg" },
                    { 19, "Light theme with a blue-green header and a sidebar on the left. Main menu in the sidebar.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<div id:updates\" \"class=\"boxed\">", "<div class=\"content\"><ul><li>", "</li></ul></div></div>", "</li>", "<h2 class=\"title\">", "</h2>", "Creative Commons Attribution 2.5", "Essence", "screenshot.jpg" },
                    { 5, "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Orange)", "screenshot.jpg" },
                    { 17, "Bright creative black-green theme.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Customize", "screenshot.jpg" },
                    { 16, "Dark gray theme with a side column on the left.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Customary", "screenshot.jpg" },
                    { 15, "Red theme with a single sidebar and a very mouth-watering title", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Culinary", "screenshot.jpg" },
                    { 14, "Bright blue theme. One side panel.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Coverage", "screenshot.jpg" },
                    { 13, "One side panel, calm colors.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Conjunction", "screenshot.jpg" },
                    { 12, "Sidebar on the left, blue tones, bright orange illumination of the main menu. In the sidebar above the list of pages, the \"Short about the site\" block, editable via additional fields.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<ul><li>", "</li></ul>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Condition", "screenshot.jpg" },
                    { 11, "One side panel, green and purple tones, drawing on the theme of vegetation.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Clear Breeze", "screenshot.jpg" },
                    { 10, "Very beautiful theme in light refreshing colors. One side panel.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<ul><li>", "</li></ul>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Cattleya", "screenshot.jpg" },
                    { 9, "Black theme with one sidebar. Main menu in the sidebar.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Boosting", "screenshot.jpg" },
                    { 8, "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Yellow)", "screenshot.jpg" },
                    { 21, "Bright red theme.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Filaments", "screenshot.jpg" },
                    { 18, "Nice theme in beige tones with a side column on the left.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Deviation", "screenshot.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_blocks_site_id",
                table: "blocks",
                column: "site_id");

            migrationBuilder.CreateIndex(
                name: "ix_files_site_id",
                table: "files",
                column: "site_id");

            migrationBuilder.CreateIndex(
                name: "ix_files_user_id",
                table: "files",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_pages_site_id",
                table: "pages",
                column: "site_id");

            migrationBuilder.CreateIndex(
                name: "ix_section_names_site_id",
                table: "section_names",
                column: "site_id");

            migrationBuilder.CreateIndex(
                name: "ix_sites_template_id",
                table: "sites",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_sites_user_id",
                table: "sites",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_templates_name",
                table: "templates",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_scripts_site_id",
                table: "user_scripts",
                column: "site_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blocks");

            migrationBuilder.DropTable(
                name: "email_templates");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "section_names");

            migrationBuilder.DropTable(
                name: "template_keys");

            migrationBuilder.DropTable(
                name: "user_scripts");

            migrationBuilder.DropTable(
                name: "sites");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
