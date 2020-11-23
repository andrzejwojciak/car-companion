using carcompanion.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<bool> AddExpenseAsync(Car car, Expense newExpense);
    }
}