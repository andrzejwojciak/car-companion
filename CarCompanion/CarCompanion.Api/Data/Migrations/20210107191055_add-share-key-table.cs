using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace carcompanion.Data.Migrations
{
    public partial class addsharekeytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCarRoleId",
                table: "UserCars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserCarRoles",
                columns: table => new
                {
                    UserCarRoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCarRoles", x => x.UserCarRoleId);
                });

            migrationBuilder.CreateTable(
                name: "ShareKeys",
                columns: table => new
                {
                    ShareKeyId = table.Column<Guid>(nullable: false),
                    IssuerId = table.Column<Guid>(nullable: false),
                    CarId = table.Column<Guid>(nullable: false),
                    UserCarRoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareKeys", x => x.ShareKeyId);
                    table.ForeignKey(
                        name: "FK_ShareKeys_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareKeys_Users_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareKeys_UserCarRoles_UserCarRoleId",
                        column: x => x.UserCarRoleId,
                        principalTable: "UserCarRoles",
                        principalColumn: "UserCarRoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UserCarRoles",
                column: "UserCarRoleId",
                value: "owner");

            migrationBuilder.InsertData(
                table: "UserCarRoles",
                column: "UserCarRoleId",
                value: "editor");

            migrationBuilder.InsertData(
                table: "UserCarRoles",
                column: "UserCarRoleId",
                value: "viewer");

            migrationBuilder.CreateIndex(
                name: "IX_UserCars_UserCarRoleId",
                table: "UserCars",
                column: "UserCarRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareKeys_CarId",
                table: "ShareKeys",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareKeys_IssuerId",
                table: "ShareKeys",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareKeys_UserCarRoleId",
                table: "ShareKeys",
                column: "UserCarRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCars_UserCarRoles_UserCarRoleId",
                table: "UserCars",
                column: "UserCarRoleId",
                principalTable: "UserCarRoles",
                principalColumn: "UserCarRoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCars_UserCarRoles_UserCarRoleId",
                table: "UserCars");

            migrationBuilder.DropTable(
                name: "ShareKeys");

            migrationBuilder.DropTable(
                name: "UserCarRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserCars_UserCarRoleId",
                table: "UserCars");

            migrationBuilder.DropColumn(
                name: "UserCarRoleId",
                table: "UserCars");
        }
    }
}
