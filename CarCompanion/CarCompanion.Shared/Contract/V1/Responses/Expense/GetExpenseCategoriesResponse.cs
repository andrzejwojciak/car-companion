using System.Collections.Generic;

namespace CarCompanion.Shared.Contract.V1.Responses.Expense
{
    public class GetExpenseCategoriesResponse 
    {
        public IEnumerable<ExpenseCategoryResponse> ExpenseCategories { get; set; }
    }
}