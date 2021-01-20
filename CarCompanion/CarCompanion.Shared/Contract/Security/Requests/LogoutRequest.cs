using System;
using System.ComponentModel.DataAnnotations;

namespace CarCompanion.Shared.Contract.Security.Requests
{
    public class LogoutRequest
    {
        [Required]        
        public Guid RefreshToken { get; set; }
    }
}