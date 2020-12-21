using carcompanion.Contract.V1.Requests.Expense;
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

        Task<ServiceResult> UpdateExpenseByIdAsync(Guid carId, Guid expesneId, Guid userId, IUpdateExpenseRequest request);

        Task<ServiceResult> DeleteExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId);
    }
}