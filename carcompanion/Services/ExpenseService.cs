using System;
using System.Linq;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using carcompanion.Services.Interfaces;
using carcompanion.Results;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(ApplicationDbContext context, IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
            _context = context;            
        }

        //TODO: Add userId to expense owner or smth
        public async Task<bool> AddExpenseAsync(User user, Car car, Expense newExpense)
        {
            newExpense.Car = car;
            newExpense.User = user;
            await _context.Expenses.AddAsync(newExpense);

            var added = await _context.SaveChangesAsync();
            return added > 0 ? true : false;
        }

        public async Task<DeleteExpenseResult> DeleteExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId)
        {            
            var result = new DeleteExpenseResult();            
            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);

            if(expense == null)                
            {                
                result.Success = false;
                result.ErrorMessage = "Expense doesn't exists!";
                result.StatusCode = 404;
                return result;
            }
            
            var userHasCar = expense.Car.UserCars.FirstOrDefault(u => u.UserId == userId);
            
            if(userHasCar == null)                
            {                
                result.Success = false; 
                result.ErrorMessage = "User has no permision to do that!";
                result.StatusCode = 401;
                return result;
            }

            if(expense.CarId != carId)
            {              
                result.Success = false;
                result.ErrorMessage = "This is not this car expense";
                result.StatusCode = 400;
                return result;
            }
            
            var deleted = await _expenseRepository.DeleteExpenseAsync(expense);
            result.Success = deleted;

            return result;
        }

        public async Task<Expense> GetExpenseById(Guid expenseId)
        {
            var expense = await _context.Expenses.Include(c => c.Car)
                                    .FirstOrDefaultAsync( i => i.ExpenseId == expenseId);            
            return expense;                    
        }
    }
}