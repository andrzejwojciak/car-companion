using System;
using System.Linq;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using carcompanion.Services.Interfaces;
using carcompanion.Results;
using Microsoft.EntityFrameworkCore;
using carcompanion.Contract.V1.Responses.Expense;
using AutoMapper;

namespace carcompanion.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public ExpenseService(ApplicationDbContext context, IExpenseRepository expenseRepository, ICarRepository carRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _carRepository = carRepository;
            _context = context;            
            _mapper = mapper;
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

        public async Task<ServiceResult> DeleteExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId)
        {            
            var result = new ServiceResult();            
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
            result.ResponseData = new DeleteExpenseResponse { Message = "Expense deleted" };
            
            return result;
        }

        public async Task<ServiceResult> GetExpensesByCarIdAsync(Guid carId, Guid userId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            var result = new ServiceResult();

            if(car == null)                
            {                
                result.Success = false;
                result.ErrorMessage = "Car doesn't exists!";
                result.StatusCode = 404;
                result.ResponseData = null;
                return result;
            }
            
            var userHasCar = car.UserCars.FirstOrDefault(u => u.UserId == userId);            
            
            if(userHasCar == null)                
            {                
                result.Success = false; 
                result.ErrorMessage = "User has no permision to do that!";
                result.StatusCode = 401;
                result.ResponseData = null;
                return result;
            }

            result.Success = true;
            result.ResponseData = _mapper.Map<Car, GetExpensesByCarIdResponse>(car);

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