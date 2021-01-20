using System.Collections.Generic;

namespace carcompanion.Models
{
    public class Role
    {
        public string RoleId { get; set; }
        
        public IEnumerable<User> Users { get; set; }
    }
}