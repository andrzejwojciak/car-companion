using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Models
{
    public class UserCarRole
    {
        [Key]
        public string UserCarRoleId { get; set; }  

        public ICollection<UserCar> UserCars { get; set; }        
        public ICollection<ShareKey> ShareKeys { get; set; }
    }
}