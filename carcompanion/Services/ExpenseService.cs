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
                return UnsuccessResult("Expense doesn't exists", 404);
            
            if(expense.Car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)   
                return UnsuccessResult("User doesn't have that car", 401);
            
            if(expense.CarId != carId)
                return UnsuccessResult("This is not this car expense", 400);

            return SuccessResult(_mapper.Map<ExpenseResponse>(expense), 200);
        }

        public async Task<ServiceResult> CreateExpenseAsync(Guid carId, Guid userId, Expense expense)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            
            if(car == null)
                return UnsuccessResult("Car doesn't exist", 404);            

            if(car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)
                return UnsuccessResult("User can't do that", 401);
                        
            if(!await VerifyExpenseCategory(expense.Category))
                return UnsuccessResult("Wrong expense category", 400);

            expense.Car = car;
            expense.UserId = userId; 
            var added = await _expenseRepository.CreateExpenseAsync(expense);
            
            return SuccessResult(_mapper.Map<ExpenseResponse>(expense), 201);
        }

        public async Task<ServiceResult> DeleteExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId)
        {                 
            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);

            if(expense == null)             
                return UnsuccessResult("Expense doesn't exists!", 404);
            
            var userHasCar = expense.Car.UserCars.FirstOrDefault(u => u.UserId == userId);
            
            if(userHasCar == null)         
                return UnsuccessResult( "User has no permision to do that!", 401);

            if(expense.CarId != carId)
                return UnsuccessResult("This is not this car expense", 400);

            if(await _expenseRepository.DeleteExpenseAsync(expense))
                return SuccessResult(new DeleteExpenseResponse { Message = "Expense deleted" }, 200);
            
            return UnsuccessResult("Something went wrong", 500);
        }

        public async Task<ServiceResult> GetExpensesByCarIdAsync(Guid carId, Guid userId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);

            if(car == null)              
                return UnsuccessResult("Car doesn't exists!", 404);          
            
            if(car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)             
                return UnsuccessResult("User has no permision to do that!", 401);

            return SuccessResult(_mapper.Map<Car, GetExpensesByCarIdResponse>(car), 200);
        }

        public async Task<ServiceResult> UpdateExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId, IUpdateExpenseRequest request)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);

            if(expense == null)
                return UnsuccessResult("Expense doesn't exist", 404);
            
            if(expense.CarId != carId)
                return UnsuccessResult("This is not this car expense", 400);
            
            if(expense.Car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)
                return UnsuccessResult("User can't do that", 401);
            
            expense = _mapper.Map(request, expense);

            if(!await VerifyExpenseCategory(expense.Category))
                return UnsuccessResult("Wrong expense category", 400);

            if(await _expenseRepository.UpdateExpenseAsync(expense))
                return SuccessResult(_mapper.Map<ExpenseResponse>(expense), 200);

            return UnsuccessResult("Something went wrong", 500);
        }

        public async Task<ServiceResult> GetExpenseCategoriesAsync()
        {
            var expenseCategories = await _expenseRepository.GetExpenseCatagoriesAsync();
            var response = new GetExpenseCategoriesResponse();
            response.ExpenseCategories = _mapper.Map<IEnumerable<ExpenseCategoryResponse>>(expenseCategories);
            return SuccessResult(response, 200);
        }

        private async Task<bool> VerifyExpenseCategory(string expenseCategory)
        {
            var categories = await _expenseRepository.GetExpenseCatagoriesAsync();
            return categories.FirstOrDefault(i => i.ExpenseCategoryId.Equals(expenseCategory)) == null ? false : true;            
        }

        private ServiceResult UnsuccessResult(string errorMessage, int? statusCode)
        {
            return new ServiceResult { Success = false, ErrorMessage = errorMessage, StatusCode = statusCode != null ? (int)statusCode : 400};
        }
        
        private ServiceResult SuccessResult(IResponseData response, int? statusCode)
        {
            return new ServiceResult { Success = true, ResponseData = response, StatusCode = statusCode != null ? (int)statusCode : 200};
        }
    }
}