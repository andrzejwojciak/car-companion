using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Models
{
    public class User
    {
        public User()
        {
            AddedDate = DateTime.UtcNow;
            EmailConfirmed = false;
        }

        [Key]
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime AddedDate { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<UserCar> UserCars { get; set; }
        
        
    }
}