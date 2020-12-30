using System;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.Security.Requests
{
    public class LogoutRequest
    {
        [Required]        
        public Guid RefreshToken { get; set; }
    }
}