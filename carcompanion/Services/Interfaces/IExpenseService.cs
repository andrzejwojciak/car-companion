using carcompanion.Models;
using carcompanion.Results;
using System;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<ServiceResult> GetExpensesByCarIdAsync(Guid carId, Guid userId);
        Task<ServiceResult> DeleteExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId);
        Task<ServiceResult> CreateExpenseAsync(Guid carId, Guid userId, Expense expense);
        Task<Expense> GetExpenseById(Guid expenseId);
    }
}