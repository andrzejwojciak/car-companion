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

        public async Task<ServiceResult> CreateExpenseAsync(Guid carId, Guid userId, Expense expense)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            
            if(car == null)
                return UnsuccessResult("Car doesn't exist", 404);            

            if(car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)
                return UnsuccessResult("User can't do that", 401);

            expense.Car = car;
            expense.UserId = userId; 
            var added = await _expenseRepository.CreateExpenseAsync(expense);
            
            return SuccessResult(_mapper.Map<ExpenseResponse>(expense), 201);
        }

        public async Task<ServiceResult> DeleteExpenseByIdAsync(Guid carId, Guid expenseId, Guid userId)
        {            
            var result = new ServiceResult();            
            var expense = await _expenseRepository.GetExpenseByIdAsync(expenseId);

            if(expense == null)             
                return UnsuccessResult("Expense doesn't exists!", 404);
            
            var userHasCar = expense.Car.UserCars.FirstOrDefault(u => u.UserId == userId);
            
            if(userHasCar == null)         
                return UnsuccessResult( "User has no permision to do that!", 401);

            if(expense.CarId != carId)
                return UnsuccessResult("This is not this car expense", 400);

            result.Success = await _expenseRepository.DeleteExpenseAsync(expense);;
            result.ResponseData = new DeleteExpenseResponse { Message = "Expense deleted" };
            
            return result;
        }

        public async Task<ServiceResult> GetExpensesByCarIdAsync(Guid carId, Guid userId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            var result = new ServiceResult();

            if(car == null)              
                return UnsuccessResult("Car doesn't exists!", 404);

            var userHasCar = car.UserCars.FirstOrDefault(u => u.UserId == userId);            
            
            if(userHasCar == null)             
                return UnsuccessResult("User has no permision to do that!", 401);

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

        private ServiceResult UnsuccessResult(string errorMessage, int statusCode)
        {
            return new ServiceResult { Success = false, ErrorMessage = errorMessage, StatusCode = statusCode};
        }

        private ServiceResult SuccessResult(IResponseData response, int? statusCode)
        {
            return new ServiceResult { Success = true, ResponseData = response, StatusCode = statusCode != null ? (int)statusCode : 200};
        }
    }
}