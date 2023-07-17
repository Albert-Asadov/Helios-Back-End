using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class IntroductionCardDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntroductionCards_Introductions_introductionId",
                table: "IntroductionCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntroductionCards",
                table: "IntroductionCards");

            migrationBuilder.RenameTable(
                name: "IntroductionCards",
                newName: "introductionCards");

            migrationBuilder.RenameIndex(
                name: "IX_IntroductionCards_introductionId",
                table: "introductionCards",
                newName: "IX_introductionCards_introductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_introductionCards",
                table: "introductionCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_introductionCards_Introductions_introductionId",
                table: "introductionCards",
                column: "introductionId",
                principalTable: "Introductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_introductionCards_Introductions_introductionId",
                table: "introductionCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_introductionCards",
                table: "introductionCards");

            migrationBuilder.RenameTable(
                name: "introductionCards",
                newName: "IntroductionCards");

            migrationBuilder.RenameIndex(
                name: "IX_introductionCards_introductionId",
                table: "IntroductionCards",
                newName: "IX_IntroductionCards_introductionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntroductionCards",
                table: "IntroductionCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntroductionCards_Introductions_introductionId",
                table: "IntroductionCards",
                column: "introductionId",
                principalTable: "Introductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
