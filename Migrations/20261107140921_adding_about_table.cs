using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYAcademy_PortfolioProject.Migrations
{
    /// <inheritdoc />
    public partial class adding_about_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    AboutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutExperience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutMainImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutNameSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutProfession = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutCVUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.AboutId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");
        }
    }
}
