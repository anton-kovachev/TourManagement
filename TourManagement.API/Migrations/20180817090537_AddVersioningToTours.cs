using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourManagement.API.Migrations
{
    public partial class AddVersioningToTours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Tours",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Tours");
        }
    }
}
