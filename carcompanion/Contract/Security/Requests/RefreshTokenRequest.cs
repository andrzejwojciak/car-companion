using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.Security.Requests
{
    public class RefreshTokenRequest
    {        
        [Required]
        public string RefreshToken { get; set; }
    }
}