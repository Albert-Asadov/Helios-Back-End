using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class IntroductionCardDBdelRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_introductionCards_Introductions_introductionId",
                table: "introductionCards");

            migrationBuilder.DropIndex(
                name: "IX_introductionCards_introductionId",
                table: "introductionCards");

            migrationBuilder.DropColumn(
                name: "introductionId",
                table: "introductionCards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "introductionId",
                table: "introductionCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_introductionCards_introductionId",
                table: "introductionCards",
                column: "introductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_introductionCards_Introductions_introductionId",
                table: "introductionCards",
                column: "introductionId",
                principalTable: "Introductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
