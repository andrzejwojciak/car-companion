using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Repositories.Interfaces
{
    public interface IExpenseRepository
    {        
        Task<bool> CreateExpenseAsync(Expense expense);
        Task<Expense> GetExpenseByIdAsync(Guid expenseId);      
        Task<List<Expense>> GetExpensesByCarIdAsync(Guid carId, DateTime startDate, DateTime endDate);  
        Task<bool> UpdateExpenseAsync(Expense expense);
        Task<bool> DeleteExpenseAsync(Expense expense);       
        Task<IEnumerable<ExpenseCategory>> GetExpenseCatagoriesAsync();
    }
}