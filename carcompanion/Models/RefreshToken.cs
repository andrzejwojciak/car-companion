using System;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Models
{
    public class RefreshToken
    {
        public RefreshToken()
        {
            Used = false;
            CreatedDate = DateTime.Now;
        }

        [Key]
        public Guid RefreshTokenId { get; set; }
        public Guid AccessTokenJti { get; set; }   
        public Guid UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Used { get; set; }
        
    }
}