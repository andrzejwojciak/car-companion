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
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }      
        public string Category { get; set; }        
        public DateTime Date { get; set; }
        public int? MileageInterval { get; set; }        
        public DateTime? EndOfDateInterval { get; set; }  

        public DateTime AddedDate { get; set; }
        public User User { get; set; }        
        public Car Car { get; set; }
        
    }
}