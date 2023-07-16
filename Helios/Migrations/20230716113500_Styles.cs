using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class Styles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categoryStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoryStyles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    textContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StylesCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stylesId = table.Column<int>(type: "int", nullable: false),
                    CategoryStyleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StylesCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StylesCategories_categoryStyles_CategoryStyleId",
                        column: x => x.CategoryStyleId,
                        principalTable: "categoryStyles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StylesCategories_Styles_stylesId",
                        column: x => x.stylesId,
                        principalTable: "Styles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StylesImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: true),
                    stylesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StylesImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StylesImages_Styles_stylesId",
                        column: x => x.stylesId,
                        principalTable: "Styles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StylesCategories_CategoryStyleId",
                table: "StylesCategories",
                column: "CategoryStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_StylesCategories_stylesId",
                table: "StylesCategories",
                column: "stylesId");

            migrationBuilder.CreateIndex(
                name: "IX_StylesImages_stylesId",
                table: "StylesImages",
                column: "stylesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StylesCategories");

            migrationBuilder.DropTable(
                name: "StylesImages");

            migrationBuilder.DropTable(
                name: "categoryStyles");

            migrationBuilder.DropTable(
                name: "Styles");
        }
    }
}
