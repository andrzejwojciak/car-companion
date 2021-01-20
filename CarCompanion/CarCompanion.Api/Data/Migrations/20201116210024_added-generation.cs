using Microsoft.EntityFrameworkCore.Migrations;

namespace carcompanion.Data.Migrations
{
    public partial class addedgeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Generation",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Generation",
                table: "Cars");
        }
    }
}
