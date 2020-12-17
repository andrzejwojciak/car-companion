using carcompanion.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<Expense> GetExpenseById(Guid expenseId);
        Task<bool> AddExpenseAsync(User user, Car car, Expense newExpense);
    }
}