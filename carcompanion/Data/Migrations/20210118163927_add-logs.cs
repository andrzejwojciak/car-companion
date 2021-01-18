using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace carcompanion.Data.Migrations
{
    public partial class addlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    LogEvent = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ClientIp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AddedDate", "Email", "EmailConfirmed", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("018bc3cc-24a1-45ac-bdd8-4ee8da30ff81"), new DateTime(2021, 1, 18, 17, 39, 26, 69, DateTimeKind.Local).AddTicks(7707), "admin@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "admin" },
                    { new Guid("9162e011-93d5-4bab-8599-46c17dfd0aa7"), new DateTime(2021, 1, 18, 17, 39, 26, 75, DateTimeKind.Local).AddTicks(9411), "superuser@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "superUser" },
                    { new Guid("00894133-c04f-4064-8d82-d7b54018f456"), new DateTime(2021, 1, 18, 17, 39, 26, 75, DateTimeKind.Local).AddTicks(9572), "firstuser@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "user" },
                    { new Guid("5a8e3d5b-3f60-4b4f-bdba-fa8564a34bc0"), new DateTime(2021, 1, 18, 17, 39, 26, 75, DateTimeKind.Local).AddTicks(9587), "secounduser@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "user" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("00894133-c04f-4064-8d82-d7b54018f456"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("018bc3cc-24a1-45ac-bdd8-4ee8da30ff81"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("5a8e3d5b-3f60-4b4f-bdba-fa8564a34bc0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9162e011-93d5-4bab-8599-46c17dfd0aa7"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AddedDate", "Email", "EmailConfirmed", "Password", "RoleId" },
                values: new object[,]
                {
                    { new Guid("d9b78b4b-9d30-4c7f-8b98-951a4539246f"), new DateTime(2021, 1, 16, 18, 8, 22, 778, DateTimeKind.Local).AddTicks(2494), "admin@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "admin" },
                    { new Guid("d561f14f-23d0-4fbf-8539-415ca52d5337"), new DateTime(2021, 1, 16, 18, 8, 22, 781, DateTimeKind.Local).AddTicks(8343), "superuser@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "superUser" },
                    { new Guid("d56faaab-a0df-4124-a3ff-08164eb27962"), new DateTime(2021, 1, 16, 18, 8, 22, 781, DateTimeKind.Local).AddTicks(8399), "firstuser@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "user" },
                    { new Guid("1c5ce31e-f130-406b-9c1e-56d1c75c629f"), new DateTime(2021, 1, 16, 18, 8, 22, 781, DateTimeKind.Local).AddTicks(8409), "secounduser@example.com", false, "b7F7UYpUwpYZBbIsp0McRFp+/LAtS9e8zZsDF1sYKbw=", "user" }
                });
        }
    }
}
