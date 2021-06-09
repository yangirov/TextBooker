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
				name: "section_names",
				columns: table => new
				{
					id = table.Column<string>(nullable: false),
					content = table.Column<string>(nullable: true),
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
					{ 4, "Заголовок о сайте" },
					{ 3, "Заголовок списка страниц" },
					{ 2, "Подзаголовок" },
					{ 1, "Заголовок главного меню" },
					{ 5, "Коротко о сайте" },
					{ 6, "Подвал" }
				});

			migrationBuilder.InsertData(
				table: "templates",
				columns: new[] { "id", "about", "author", "author_url", "block_begin", "block_content_begin", "block_content_end", "block_end", "block_title_begin", "block_title_end", "license", "name", "screenshot" },
				values: new object[,]
				{
					{ 7, "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Red)", "screenshot.jpg" },
					{ 1, "Это стандартная тема с блоками и одной боковой панелью справа. В заголовке старницы - рисунок модели атома. Прекрасная тема для электронного учебника по физике.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<div class=\"boxed\">", "<div class=\"content\">", "</div>", "</div>", "<h2 class=\"title\">", "</h2>", "Creative Commons Attribution 2.5", "Atomohost", "screenshot.jpg" },
					{ 2, "Простая и современная светлая тема.", "Switchroyale", "http://www.switchroyale.com/", "<li> ", "<div>", "</div>", "</li>", "<h2>", "</h2>", null, "Azul", "screenshot.jpg" },
					{ 3, "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Blue)", "screenshot.jpg" },
					{ 4, "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Green)", "screenshot.jpg" },
					{ 31, "Яркая креативная светло-голубая тема с блоком \"О сайте\".", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Snowglass", "screenshot.jpg" },
					{ 30, "Тема гор, синие тона, главное меню в боковой колонке.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Mountain Breeze", "screenshot.jpg" },
					{ 29, "Очень красивая яркая черно-зеленая тема с одной боковой колонкой.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<ul><li><p>", "</p></li></ul>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Marked", "screenshot.jpg" },
					{ 28, "Серо-зеленая тема с левой боковой колонкой и рисунком в заголовке на растительную тему.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<ul><li><p>", "</p></li></ul>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Lily Pads", "screenshot.jpg" },
					{ 27, "Светло-оранжевая тема с одной боковой колонкой.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Leaf Brush", "screenshot.jpg" },
					{ 26, "Строгая серая тема с боковой колонкой слева. Главное меню в боковой колонке.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Indicator", "screenshot.jpg" },
					{ 25, "Светлая голубая тема с одной боковой колонкой.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Indication", "screenshot.jpg" },
					{ 24, "Дерево и металл.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Improved", "screenshot.jpg" },
					{ 23, "Светлая тема в коричневых тонах с одной боковой панелью.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Guarantee", "screenshot.jpg" },
					{ 6, "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Pink)", "screenshot.jpg" },
					{ 22, "Красивая оранжево-зеленая тема с цветами в заголовке.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Fresh Scent", "screenshot.jpg" },
					{ 20, "Светло-голубая тема с правой боковой колонкой.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Exploitable", "screenshot.jpg" },
					{ 19, "Светлая тема с сине-зеленым заголовком и боковой панелью слева. Главное меню в боковой панели.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<div id:updates\" \"class=\"boxed\">", "<div class=\"content\"><ul><li>", "</li></ul></div></div>", "</li>", "<h2 class=\"title\">", "</h2>", "Creative Commons Attribution 2.5", "Essence", "screenshot.jpg" },
					{ 5, "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Orange)", "screenshot.jpg" },
					{ 17, "Яркая креативная черное-зеленая тема.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Customize", "screenshot.jpg" },
					{ 16, "Темно-серая тема с боковой колонкой слева.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Customary", "screenshot.jpg" },
					{ 15, "Красная тема с одной боковой панелью и очень аппетитным заголовком", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Culinary", "screenshot.jpg" },
					{ 14, "Ярко-синяя тема. Одна боковая панель.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Coverage", "screenshot.jpg" },
					{ 13, "Одна боковая панель, спокойные цвета.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Conjunction", "screenshot.jpg" },
					{ 12, "Боковая панель слева, голубые тона, яркая оранжевая подсветка главного меню. В боковой панели над списком страниц блок \"Коротко о сайте\", редактируемый через дополнительные поля.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<ul><li>", "</li></ul>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Condition", "screenshot.jpg" },
					{ 11, "1 боковая панель, зеленые и фиолетовые тона, рисунок на тему растительности.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Clear Breeze", "screenshot.jpg" },
					{ 10, "Очень красивая тема в светлых освежающих тонах. Одна боковая панель.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<ul><li>", "</li></ul>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Cattleya", "screenshot.jpg" },
					{ 9, "Черная тема с 1 боковой панелью. Главное меню в боковой панели.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Boosting", "screenshot.jpg" },
					{ 8, "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.", "Grigoruta Adrian", "http://www.pixelstudio.ro/", "<div>", "<div>", "</div>", "</div>", "<h3>", "</h3>", null, "Belle (Yellow)", "screenshot.jpg" },
					{ 21, "Ярко-красная тема.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Filaments", "screenshot.jpg" },
					{ 18, "Приятная тема в бежевых тонах с боковой колонкой слева.", "Free CSS Templates", "http://www.freecsstemplates.org/", "<li>", "<p>", "</p>", "</li>", "<h2>", "</h2>", "Creative Commons Attribution 2.5", "Deviation", "screenshot.jpg" }
				});

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
				name: "email_templates");

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
