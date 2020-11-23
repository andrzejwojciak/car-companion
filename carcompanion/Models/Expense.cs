using System;

namespace carcompanion.Models
{
    public class Expense
    {
        public Expense()
        {
            AddedDate = DateTime.UtcNow;
        }

        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public int MileageInterval { get; set; }        
        public DateTime EndOfDateInterval { get; set; }        
        public string Category { get; set; }        
        public DateTime AddedDate { get; set; }
        
        public Car Car { get; set; }
        
    }
}