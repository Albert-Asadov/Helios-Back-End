using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class RenameAndTextContentColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CardTitle",
                table: "Blogs",
                newName: "TextContent");

            migrationBuilder.AddColumn<string>(
                name: "CardTitleDetail",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardTitleDetail",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "TextContent",
                table: "Blogs",
                newName: "CardTitle");
        }
    }
}
