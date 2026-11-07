using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MYAcademy_PortfolioProject.Migrations
{
    /// <inheritdoc />
    public partial class tableupdates_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SkillValue",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Socials",
                columns: table => new
                {
                    SocialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialPlatform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SocialIcon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socials", x => x.SocialID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Socials");

            migrationBuilder.DropColumn(
                name: "SkillValue",
                table: "Skills");
        }
    }
}
