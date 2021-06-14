using Microsoft.EntityFrameworkCore.Migrations;

namespace TextBooker.DataAccess.Migrations
{
    public partial class TemplatesTranslations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "MainMenuTitle");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "Subtitle");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 3,
                column: "name",
                value: "PageListTitle");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 4,
                column: "name",
                value: "AboutSiteTitle");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 5,
                column: "name",
                value: "AboutSiteShort");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 6,
                column: "name",
                value: "Footer");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 1,
                column: "about",
                value: "This is a standard theme with blocks and a single sidebar on the right. In the title of the starnitsa-drawing of the atom model. A great topic for an electronic physics textbook.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 2,
                column: "about",
                value: "Simple and modern light theme.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 3,
                column: "about",
                value: "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 4,
                column: "about",
                value: "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 5,
                column: "about",
                value: "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 6,
                column: "about",
                value: "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 7,
                column: "about",
                value: "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 8,
                column: "about",
                value: "Simple, light minimalistic theme in 6 different colors: blue, green, orange, pink, red and yellow.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 9,
                column: "about",
                value: "Black theme with one sidebar. Main menu in the sidebar.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 10,
                column: "about",
                value: "Very beautiful theme in light refreshing colors. One side panel.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 11,
                column: "about",
                value: "One side panel, green and purple tones, drawing on the theme of vegetation.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 12,
                column: "about",
                value: "Sidebar on the left, blue tones, bright orange illumination of the main menu. In the sidebar above the list of pages, the \"Short about the site\" block, editable via additional fields.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 13,
                column: "about",
                value: "One side panel, calm colors.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 14,
                column: "about",
                value: "Bright blue theme. One side panel.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 15,
                column: "about",
                value: "a Red theme with a single sidebar and a very mouth-watering title");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 16,
                column: "about",
                value: "Dark gray theme with a side column on the left.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 17,
                column: "about",
                value: "Bright creative black-green theme.");

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
                keyValue: 20,
                column: "about",
                value: "Light blue theme with right side column.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 21,
                column: "about",
                value: "Bright red theme.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 22,
                column: "about",
                value: "Beautiful orange-green theme with flowers in the title.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 23,
                column: "about",
                value: "Light theme in brown tones with a single sidebar.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 24,
                column: "about",
                value: "Wood and metal.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 25,
                column: "about",
                value: "Light blue theme with one side column.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 26,
                column: "about",
                value: "a Strict gray theme with a side column on the left. The main menu in the sidebar.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 27,
                column: "about",
                value: "Light orange theme with one sidebar column.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 28,
                column: "about",
                value: "Grey-green theme with left-side column and the picture in the header on vegetable theme.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 29,
                column: "about",
                value: "a Very beautiful bright green and black theme with one side column.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 30,
                column: "about",
                value: "Subject mountains, blue tone, main menu in sidebar.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 31,
                column: "about",
                value: "a Bright creative light blue theme with a block \"About site\".");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "Заголовок главного меню");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 2,
                column: "name",
                value: "Подзаголовок");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 3,
                column: "name",
                value: "Заголовок списка страниц");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 4,
                column: "name",
                value: "Заголовок о сайте");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 5,
                column: "name",
                value: "Коротко о сайте");

            migrationBuilder.UpdateData(
                table: "template_keys",
                keyColumn: "id",
                keyValue: 6,
                column: "name",
                value: "Подвал");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 1,
                column: "about",
                value: "Это стандартная тема с блоками и одной боковой панелью справа. В заголовке старницы - рисунок модели атома. Прекрасная тема для электронного учебника по физике.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 2,
                column: "about",
                value: "Простая и современная светлая тема.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 3,
                column: "about",
                value: "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 4,
                column: "about",
                value: "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 5,
                column: "about",
                value: "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 6,
                column: "about",
                value: "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 7,
                column: "about",
                value: "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 8,
                column: "about",
                value: "Простая, светлая минималистичная тема в 6 различных цветах: синий, зеленый, оранжевый, розовый, красный и желтый.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 9,
                column: "about",
                value: "Черная тема с 1 боковой панелью. Главное меню в боковой панели.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 10,
                column: "about",
                value: "Очень красивая тема в светлых освежающих тонах. Одна боковая панель.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 11,
                column: "about",
                value: "1 боковая панель, зеленые и фиолетовые тона, рисунок на тему растительности.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 12,
                column: "about",
                value: "Боковая панель слева, голубые тона, яркая оранжевая подсветка главного меню. В боковой панели над списком страниц блок \"Коротко о сайте\", редактируемый через дополнительные поля.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 13,
                column: "about",
                value: "Одна боковая панель, спокойные цвета.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 14,
                column: "about",
                value: "Ярко-синяя тема. Одна боковая панель.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 15,
                column: "about",
                value: "Красная тема с одной боковой панелью и очень аппетитным заголовком");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 16,
                column: "about",
                value: "Темно-серая тема с боковой колонкой слева.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 17,
                column: "about",
                value: "Яркая креативная черное-зеленая тема.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 18,
                column: "about",
                value: "Приятная тема в бежевых тонах с боковой колонкой слева.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 19,
                column: "about",
                value: "Светлая тема с сине-зеленым заголовком и боковой панелью слева. Главное меню в боковой панели.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 20,
                column: "about",
                value: "Светло-голубая тема с правой боковой колонкой.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 21,
                column: "about",
                value: "Ярко-красная тема.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 22,
                column: "about",
                value: "Красивая оранжево-зеленая тема с цветами в заголовке.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 23,
                column: "about",
                value: "Светлая тема в коричневых тонах с одной боковой панелью.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 24,
                column: "about",
                value: "Дерево и металл.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 25,
                column: "about",
                value: "Светлая голубая тема с одной боковой колонкой.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 26,
                column: "about",
                value: "Строгая серая тема с боковой колонкой слева. Главное меню в боковой колонке.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 27,
                column: "about",
                value: "Светло-оранжевая тема с одной боковой колонкой.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 28,
                column: "about",
                value: "Серо-зеленая тема с левой боковой колонкой и рисунком в заголовке на растительную тему.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 29,
                column: "about",
                value: "Очень красивая яркая черно-зеленая тема с одной боковой колонкой.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 30,
                column: "about",
                value: "Тема гор, синие тона, главное меню в боковой колонке.");

            migrationBuilder.UpdateData(
                table: "templates",
                keyColumn: "id",
                keyValue: 31,
                column: "about",
                value: "Яркая креативная светло-голубая тема с блоком \"О сайте\".");
        }
    }
}
