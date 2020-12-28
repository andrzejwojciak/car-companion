using Microsoft.EntityFrameworkCore.Migrations;

namespace carcompanion.Data.Migrations
{
    public partial class addexpensecategoryseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "ExpenseCategoryId", "Description", "ParentId" },
                values: new object[,]
                {
                    { "other", null, null },
                    { "insurance", null, null },
                    { "repair", null, null },
                    { "fuel", null, null },
                    { "utilization", null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "ExpenseCategoryId",
                keyValue: "fuel");

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "ExpenseCategoryId",
                keyValue: "insurance");

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "ExpenseCategoryId",
                keyValue: "other");

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "ExpenseCategoryId",
                keyValue: "repair");

            migrationBuilder.DeleteData(
                table: "ExpenseCategories",
                keyColumn: "ExpenseCategoryId",
                keyValue: "utilization");
        }
    }
}
