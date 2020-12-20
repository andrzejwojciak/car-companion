using System;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Expense
{    
    public class ExpenseResponse : IResponseData
    {
        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int? MileageInterval { get; set; }        
        public DateTime? EndOfDateInterval { get; set; }        
        public string Category { get; set; }        
        public DateTime AddedDate { get; set; }       
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
    }
}