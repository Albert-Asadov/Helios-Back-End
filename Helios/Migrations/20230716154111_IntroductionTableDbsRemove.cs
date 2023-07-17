using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class IntroductionTableDbsRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Introductions_LeftSideTitle",
                table: "Introductions");

            migrationBuilder.DropIndex(
                name: "IX_Introductions_RightSideTitle",
                table: "Introductions");

            migrationBuilder.AlterColumn<string>(
                name: "RightSideTitle",
                table: "Introductions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LeftSideTitle",
                table: "Introductions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RightSideTitle",
                table: "Introductions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LeftSideTitle",
                table: "Introductions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Introductions_LeftSideTitle",
                table: "Introductions",
                column: "LeftSideTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Introductions_RightSideTitle",
                table: "Introductions",
                column: "RightSideTitle");
        }
    }
}
