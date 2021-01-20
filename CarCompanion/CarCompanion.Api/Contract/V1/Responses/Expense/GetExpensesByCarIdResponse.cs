using System;
using System.Collections.Generic;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Expense
{
    public class GetExpensesByCarIdResponse : IResponseData
    {
        public Guid CarId { get; set; }
        public IEnumerable<ExpenseResponse> Expenses { get; set; }                  
    }
}