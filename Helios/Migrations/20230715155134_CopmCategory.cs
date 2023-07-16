using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class CopmCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Categories_CategoryId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_CategoryId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Components");

            migrationBuilder.AddColumn<int>(
                name: "ComponentCategoryId",
                table: "Components",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComponentCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentCategoryId",
                table: "Components",
                column: "ComponentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCategories_CategoryId",
                table: "ComponentCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_ComponentCategories_ComponentCategoryId",
                table: "Components",
                column: "ComponentCategoryId",
                principalTable: "ComponentCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_ComponentCategories_ComponentCategoryId",
                table: "Components");

            migrationBuilder.DropTable(
                name: "ComponentCategories");

            migrationBuilder.DropIndex(
                name: "IX_Components_ComponentCategoryId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ComponentCategoryId",
                table: "Components");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Components_CategoryId",
                table: "Components",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Categories_CategoryId",
                table: "Components",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
