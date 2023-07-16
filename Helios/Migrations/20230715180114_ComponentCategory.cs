using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class ComponentCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_ComponentCategories_ComponentCategoryId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_ComponentCategoryId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ComponentCategoryId",
                table: "Components");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentCategories_ComponentId",
                table: "ComponentCategories",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentCategories_Components_ComponentId",
                table: "ComponentCategories",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentCategories_Components_ComponentId",
                table: "ComponentCategories");

            migrationBuilder.DropIndex(
                name: "IX_ComponentCategories_ComponentId",
                table: "ComponentCategories");

            migrationBuilder.AddColumn<int>(
                name: "ComponentCategoryId",
                table: "Components",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentCategoryId",
                table: "Components",
                column: "ComponentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_ComponentCategories_ComponentCategoryId",
                table: "Components",
                column: "ComponentCategoryId",
                principalTable: "ComponentCategories",
                principalColumn: "Id");
        }
    }
}
