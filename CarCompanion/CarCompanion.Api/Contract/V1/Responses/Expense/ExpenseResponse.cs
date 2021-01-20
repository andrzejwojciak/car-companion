using System;
using System.ComponentModel.DataAnnotations;
using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Expense
{    
    public class ExpenseResponse : IResponseData
    {
        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int? MileageInterval { get; set; }              
        public string EndOfDateInterval { get; set; }           
        public string Date { get; set; }       
        public string Category { get; set; }    
        public Guid UserId { get; set; }
        public Guid CarId { get; set; }
    }
}