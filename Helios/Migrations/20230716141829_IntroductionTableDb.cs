using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class IntroductionTableDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Introductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeftSideTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RightSideTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardInsideTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardInsideText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Introductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntroductionImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: true),
                    introductionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntroductionImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntroductionImage_Introductions_introductionId",
                        column: x => x.introductionId,
                        principalTable: "Introductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntroductionImage_introductionId",
                table: "IntroductionImage",
                column: "introductionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntroductionImage");

            migrationBuilder.DropTable(
                name: "Introductions");
        }
    }
}
