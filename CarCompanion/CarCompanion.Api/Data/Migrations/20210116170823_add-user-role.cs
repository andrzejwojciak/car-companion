using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace carcompanion.Data.Migrations
{
    public partial class adduserrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                column: "RoleId",
                value: "user");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "RoleId",
                value: "superUser");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "RoleId",
                value: "admin");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AddedDate", "Email", "EmailConfirmed", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("d56faaab-a0df-4124-a3ff-08164eb27962"), new DateTime(2021, 1, 16, 18, 8, 22, 781, DateTimeKind.Local).AddTicks(8399), "firstuser@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "user" },
                    { new Guid("1c5ce31e-f130-406b-9c1e-56d1c75c629f"), new DateTime(2021, 1, 16, 18, 8, 22, 781, DateTimeKind.Local).AddTicks(8409), "secounduser@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "user" },
                    { new Guid("d561f14f-23d0-4fbf-8539-415ca52d5337"), new DateTime(2021, 1, 16, 18, 8, 22, 781, DateTimeKind.Local).AddTicks(8343), "superuser@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "superUser" },
                    { new Guid("d9b78b4b-9d30-4c7f-8b98-951a4539246f"), new DateTime(2021, 1, 16, 18, 8, 22, 778, DateTimeKind.Local).AddTicks(2494), "admin@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1c5ce31e-f130-406b-9c1e-56d1c75c629f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d561f14f-23d0-4fbf-8539-415ca52d5337"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d56faaab-a0df-4124-a3ff-08164eb27962"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("d9b78b4b-9d30-4c7f-8b98-951a4539246f"));

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");
        }
    }
}
