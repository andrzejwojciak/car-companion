using System;
using System.Collections.Generic;

namespace CarCompanion.Shared.Contract.V1.Responses.Expense
{
    public class GetExpensesByCarIdResponse 
    {
        public Guid CarId { get; set; }
        public IEnumerable<ExpenseResponse> Expenses { get; set; }                  
    }
}