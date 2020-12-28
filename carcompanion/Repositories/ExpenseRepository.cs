using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task<bool> CreateExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            return await SaveChangesAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(Guid expenseId)
        {
            var expense = await _context.Expenses
                    .Include(e => e.Car)
                        .ThenInclude(u => u.UserCars)
                    .FirstOrDefaultAsync( u => u.ExpenseId == expenseId);
                    
            return expense;
        }

        public async Task<bool> DeleteExpenseAsync(Expense expense)
        {
            _context.Expenses.Remove(expense);
            return await SaveChangesAsync();
        }
        
        public async Task<bool> UpdateExpenseAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<ExpenseCategory>> GetExpenseCatagoriesAsync()
        {
            return await _context.ExpenseCategories.ToListAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}