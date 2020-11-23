using System;

namespace carcompanion.Contract.V1.Responses
{
    public class GetCarExpensesResponse
    {
        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public int? MileageInterval { get; set; }        
        public DateTime? EndOfDateInterval { get; set; }        
        public string Category { get; set; }        
        public DateTime AddedDate { get; set; }        
    }
}