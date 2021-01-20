using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace carcompanion.Data.Migrations
{
    public partial class productiondatetoproducetionyearincars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionDate",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "ProductionYear",
                table: "Cars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionYear",
                table: "Cars");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionDate",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
