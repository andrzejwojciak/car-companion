using System;

namespace carcompanion.Models
{
    public class ShareKey
    {
        public ShareKey()
        {
            AddedDate = DateTime.UtcNow;
        }

        public Guid ShareKeyId { get; set; }
        public Guid IssuerId { get; set; }
        public Guid CarId { get; set; }
        public string UserCarRoleId { get; set; }
        public bool Used { get; set; }      
        public DateTime AddedDate { get; set; }
        
                 
        
        public User Issuer { get; set; }   
        public Car Car { get; set; }     
        public UserCarRole UserCarRole { get; set; }
    }
}