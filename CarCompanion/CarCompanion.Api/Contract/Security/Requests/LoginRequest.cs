using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.Security.Requests
{
    public class LoginRequest
    {
        [Required]        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}