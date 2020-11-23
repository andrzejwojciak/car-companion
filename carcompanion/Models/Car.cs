using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Models
{
    public class Car
    {
        public Car()
        {
            AddedDate = DateTime.UtcNow;
        }
        
        [Key]
        public Guid CarId { get; set; }
        public string MainName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Generation { get; set; }
        public string Plate { get; set; }
        public int Mileage { get; set; }        
        public int ProductionYear { get; set; }             
        public DateTime AddedDate { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        
        
        
        
        
    }
}