using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourManagement.API.Migrations
{
    public partial class AddVersioningToShows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "Shows",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Shows");
        }
    }
}
