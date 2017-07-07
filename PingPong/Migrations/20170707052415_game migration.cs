using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PingPong.Migrations
{
    public partial class gamemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlayerOneName",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerTwoName",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerOneName",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayerTwoName",
                table: "Games");
        }
    }
}
