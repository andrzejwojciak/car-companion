using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carcompanion.Contract.V1.Responses.Expense;
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

        public SummaryService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        
        public async Task<ServiceResult> GetSummaryByCarIdAsync(Guid carId, DateTime startDate, DateTime endDate)
        {
            if(startDate > endDate)
                return FailedResult("endDate can't be later than starDate", 400); 

            var expenses = await _expenseRepository.GetExpensesByCarIdAsync(carId, startDate, endDate); 

            if(expenses == null)
                FailedResult("Car doesn't have any expenses", 404);

            var response = GenerateGetSummaryByCarId(expenses);
            return SuccessResult(response, 200);
        }

        private GetSummaryByCarIdResponse GenerateGetSummaryByCarId(IList<Expense> expenses)
        {
            var response = new GetSummaryByCarIdResponse{ExpensesCount = expenses.Count()};        

            foreach( var expense in expenses )
            {
                response.ExpensesTotalAmount += expense.Amount;

                var categorySummary = response.CategoriesSummary.SingleOrDefault(c => c.CategoryName == expense.Category);
                if (categorySummary == null)
                {
                    var newCategorySumary = new CategorySummary { CategoryName = expense.Category, CategoryExpensesCount = 1, CategoryTotalAmount = expense.Amount};
                    response.CategoriesSummary.Add(newCategorySumary);
                }
                else
                {
                    categorySummary.CategoryExpensesCount ++;
                    categorySummary.CategoryTotalAmount += expense.Amount;
                }
            }            
            
            return response;
        }
        
        private ServiceResult FailedResult(string errorMessage, int? statusCode)
        {
            return new ServiceResult { Success = false, ErrorMessage = errorMessage, StatusCode = statusCode != null ? (int)statusCode : 400};
        }
        
        private ServiceResult SuccessResult(IResponseData response, int? statusCode)
        {
            return new ServiceResult { Success = true, ResponseData = response, StatusCode = statusCode != null ? (int)statusCode : 200};
        }
    }
}