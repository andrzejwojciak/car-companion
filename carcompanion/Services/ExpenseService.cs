using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _context;

        public ExpenseService(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<bool> AddExpenseAsync(Car car, Expense newExpense)
        {
            newExpense.Car = car;
            await _context.Expenses.AddAsync(newExpense);

            var added = await _context.SaveChangesAsync();
            return added > 0 ? true : false;
        }
        
    }
}