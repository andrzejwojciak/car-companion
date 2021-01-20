using System.ComponentModel.DataAnnotations;

namespace CarCompanion.Shared.Contract.Security.Requests
{
    public class RegisterRequest
    {        
        [Required]        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}