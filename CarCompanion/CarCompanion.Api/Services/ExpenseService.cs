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
using carcompanion.Contract.V1.Responses.Interfaces;
using carcompanion.Contract.V1.Requests.Expense;
using System.Collections.Generic;

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

        public async Task<ServiceResult> GetExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);

            if(expense == null)
                return FailedResult("Expense doesn't exists", 404);
            
            if(expense.Car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)   
                return FailedResult("User doesn't have this car", 401);
            
            if(expense.CarId != carId)
                return FailedResult("This is not this car expense", 400);

            return SuccessResult(_mapper.Map<ExpenseResponse>(expense), 200);
        }

        public async Task<ServiceResult> CreateExpenseAsync(Guid carId, Guid userId, Expense expense)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            
            if(car == null)
                return FailedResult("Car doesn't exist", 404);            

            if(car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)
                return FailedResult("User can't do that", 401);
                        
            if(!await VerifyExpenseCategory(expense.Category))
                return FailedResult("Wrong expense category", 400);

            expense.Car = car;
            expense.UserId = userId;
            if (!await _expenseRepository.CreateExpenseAsync(expense))
                return FailedResult("Something went wrong", 500);
            
            return SuccessResult(_mapper.Map<ExpenseResponse>(expense), 201);
        }

        public async Task<ServiceResult> DeleteExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId)
        {                 
            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);

            if(expense == null)             
                return FailedResult("Expense doesn't exists!", 404);
            
            var userHasCar = expense.Car.UserCars.FirstOrDefault(u => u.UserId == userId);
            
            if(userHasCar == null)         
                return FailedResult( "User has no permission to do that!", 401);

            if(expense.CarId != carId)
                return FailedResult("This is not this car expense", 400);

            if(await _expenseRepository.DeleteExpenseAsync(expense))
                return SuccessResult(new DeleteExpenseResponse { Message = "Expense deleted" }, 200);
            
            return FailedResult("Something went wrong", 500);
        }

        public async Task<ServiceResult> GetExpensesByCarIdAsync(Guid carId, Guid userId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);

            if(car == null)              
                return FailedResult("Car doesn't exists!", 404);          
            
            if(car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)             
                return FailedResult("User has no permission to do that!", 401);

            return SuccessResult(_mapper.Map<Car, GetExpensesByCarIdResponse>(car), 200);
        }

        public async Task<ServiceResult> UpdateExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId, IUpdateExpenseRequest request)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);

            if(expense == null)
                return FailedResult("Expense doesn't exist", 404);
            
            if(expense.CarId != carId)
                return FailedResult("This is not this car expense", 400);
            
            if(expense.Car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)
                return FailedResult("User can't do that", 401);
            
            expense = _mapper.Map(request, expense);

            if(!await VerifyExpenseCategory(expense.Category))
                return FailedResult("Wrong expense category", 400);

            if(await _expenseRepository.UpdateExpenseAsync(expense))
                return SuccessResult(_mapper.Map<ExpenseResponse>(expense), 200);

            return FailedResult("Something went wrong", 500);
        }

        public async Task<ServiceResult> GetExpenseCategoriesAsync()
        {
            var expenseCategories = await _expenseRepository.GetExpenseCategoriesAsync();
            var response = new GetExpenseCategoriesResponse
            {
                ExpenseCategories = _mapper.Map<IEnumerable<ExpenseCategoryResponse>>(expenseCategories)
            };
            return SuccessResult(response, 200);
        }

        private async Task<bool> VerifyExpenseCategory(string expenseCategory)
        {
            var categories = await _expenseRepository.GetExpenseCategoriesAsync();
            return categories.FirstOrDefault(i => i.ExpenseCategoryId.Equals(expenseCategory)) != null;            
        }

        private static ServiceResult FailedResult(string errorMessage, int statusCode)
            => new ServiceResult
                {Success = false, Status = statusCode, ErrorMessage = errorMessage};

        private static ServiceResult SuccessResult(IResponseData responseData, int statusCode)
            => new ServiceResult {Success = true, Status = statusCode, ResponseData = responseData};
        
        
    }
}