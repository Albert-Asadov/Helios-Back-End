﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helios.Migrations
{
    public partial class cartTitleBlogColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cartTitleBlog",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cartTitleBlog",
                table: "Blogs");
        }
    }
}
