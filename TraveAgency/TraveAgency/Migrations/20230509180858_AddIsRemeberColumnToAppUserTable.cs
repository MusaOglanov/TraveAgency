﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TraveAgency.Migrations
{
    public partial class AddIsRemeberColumnToAppUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemember",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemember",
                table: "AspNetUsers");
        }
    }
}
