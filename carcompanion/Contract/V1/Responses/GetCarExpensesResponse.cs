using System;

namespace carcompanion.Contract.V1.Responses
{
    public class GetCarExpensesResponse
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