using System.Collections.Generic;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Expense
{
    public class GetExpenseCategoriesResponse : IResponseData
    {
        public IEnumerable<ExpenseCategoryResponse> ExpenseCategories { get; set; }
    }
}