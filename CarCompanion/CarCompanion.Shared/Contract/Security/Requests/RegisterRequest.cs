using System.ComponentModel.DataAnnotations;

namespace CarCompanion.Shared.Contract.Security.Requests
{
    public class RegisterRequest
    {        
        [Required]        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}