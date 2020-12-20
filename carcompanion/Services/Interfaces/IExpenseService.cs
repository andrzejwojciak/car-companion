using carcompanion.Models;
using carcompanion.Results;
using System;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<ServiceResult> CreateExpenseAsync(Guid carId, Guid userId, Expense expense);
        Task<ServiceResult> GetExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId);
        Task<ServiceResult> GetExpensesByCarIdAsync(Guid carId, Guid userId);

        //Task<ServiceResult> UpdateExpenseById(Guid carId, Guid expesneId, Guid userId);
        Task<ServiceResult> DeleteExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId);
        Task<Expense> GetExpenseById(Guid expenseId);
    }
}