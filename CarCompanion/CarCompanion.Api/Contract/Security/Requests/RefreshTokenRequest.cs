using System;
using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.Security.Requests
{
    public class RefreshTokenRequest
    {        
        [Required]
        public string AccessToken { get; set; }        
        
        [Required]
        public Guid RefreshToken { get; set; }
    }
}