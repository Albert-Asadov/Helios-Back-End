using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class IntroductionCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardInsideText",
                table: "Introductions");

            migrationBuilder.DropColumn(
                name: "CardInsideTitle",
                table: "Introductions");

            migrationBuilder.CreateTable(
                name: "IntroductionCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardInsideTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardInsideText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    introductionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntroductionCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntroductionCards_Introductions_introductionId",
                        column: x => x.introductionId,
                        principalTable: "Introductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntroductionCards_introductionId",
                table: "IntroductionCards",
                column: "introductionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntroductionCards");

            migrationBuilder.AddColumn<string>(
                name: "CardInsideText",
                table: "Introductions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardInsideTitle",
                table: "Introductions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
