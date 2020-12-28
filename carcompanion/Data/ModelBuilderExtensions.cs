using carcompanion.Models;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseCategory>().HasData(
                new ExpenseCategory { ExpenseCategoryId = "Insurance1", Description = "Description1", ParentId = null },
                new ExpenseCategory { ExpenseCategoryId = "Insurance2", Description = "Description2", ParentId = null },
                new ExpenseCategory { ExpenseCategoryId = "Insurance3", Description = "Description3", ParentId = "Insurance1" },
                new ExpenseCategory { ExpenseCategoryId = "Insurance4", Description = "Description4", ParentId = "Insurance2" },
                new ExpenseCategory { ExpenseCategoryId = "Insurance5", Description = "Description5", ParentId = "Insurance2" }
            );
        }
    }
}