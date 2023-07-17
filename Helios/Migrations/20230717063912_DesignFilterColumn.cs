using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class DesignFilterColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DesignFilter",
                table: "Blogs",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesignFilter",
                table: "Blogs");
        }
    }
}
