using carcompanion.Models;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseCategory>().HasData(
                new ExpenseCategory { ExpenseCategoryId = "other", Description = null, ParentId = null },
                new ExpenseCategory { ExpenseCategoryId = "insurance", Description = null, ParentId = null },
                new ExpenseCategory { ExpenseCategoryId = "repair", Description = null, ParentId = null },
                new ExpenseCategory { ExpenseCategoryId = "fuel", Description = null, ParentId = null },       
                new ExpenseCategory { ExpenseCategoryId = "utilization", Description = null, ParentId = null }                
            );
        }
    }
}