using carcompanion.Models;
using carcompanion.Results;
using System;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<DeleteExpenseResult> DeleteExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId);
        Task<Expense> GetExpenseById(Guid expenseId);
        Task<bool> AddExpenseAsync(User user, Car car, Expense newExpense);
    }
}