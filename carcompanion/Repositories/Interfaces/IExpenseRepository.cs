using System;
using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Repositories.Interfaces
{
    public interface IExpenseRepository
    {        
        Task<bool> CreateExpenseAsync(Expense expense);
        Task<Expense> GetExpenseByIdAsync(Guid expenseId);        
        Task<bool> UpdateExpenseAsync(Expense expense);
        Task<bool> DeleteExpenseAsync(Expense expense);         
    }
}