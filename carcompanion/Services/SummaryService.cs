using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carcompanion.Contract.V1.Responses.Interfaces;
using carcompanion.Contract.V1.Responses.Summary;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using carcompanion.Results;
using carcompanion.Services.Interfaces;

namespace carcompanion.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICarRepository _carRepository;

        public SummaryService(IExpenseRepository expenseRepository, ICarRepository carRepository)
        {
            _expenseRepository = expenseRepository;
            _carRepository = carRepository;
        }

        public async Task<ServiceResult> GetSummaryByCarIdAsync(Guid carId, Guid userId, DateTime startDate,
            DateTime endDate)
        {
            if (startDate > endDate)
                return FailedResult("endDate can't be later than starDate", 400);

            var expenses = await _expenseRepository.GetExpensesByCarIdAsync(carId, startDate, endDate);

            if (expenses == null)
                FailedResult("Car doesn't have any expenses", 404);

            var response = GenerateGetSummaryByCarId(expenses);
            return SuccessResult(response, 200);
        }

        public async Task<bool> UserHasCarAsync(Guid carId, Guid userId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);
            var userCar = car.UserCars.FirstOrDefault(user => user.UserId == userId);
            return userCar != null;
        }

        private static GetSummaryByCarIdResponse GenerateGetSummaryByCarId(IList<Expense> expenses)
        {
            var response = new GetSummaryByCarIdResponse {ExpensesCount = expenses.Count()};

            foreach (var expense in expenses)
            {
                response.ExpensesTotalAmount += expense.Amount;

                var categorySummary =
                    response.CategoriesSummary.SingleOrDefault(c => c.CategoryName == expense.Category);

                if (categorySummary == null)
                {
                    var newCategorySummary = new CategorySummary
                    {
                        CategoryName = expense.Category, CategoryExpensesCount = 1, CategoryTotalAmount = expense.Amount
                    };
                    response.CategoriesSummary.Add(newCategorySummary);
                }
                else
                {
                    categorySummary.CategoryExpensesCount++;
                    categorySummary.CategoryTotalAmount += expense.Amount;
                }
            }

            return response;
        }

        private static ServiceResult FailedResult(string errorMessage, int statusCode)
            => new ServiceResult
                {Success = false, Status = statusCode, ErrorMessage = errorMessage};

        private static ServiceResult SuccessResult(IResponseData responseData, int statusCode)
            => new ServiceResult {Success = true, Status = statusCode, ResponseData = responseData};
    }
}