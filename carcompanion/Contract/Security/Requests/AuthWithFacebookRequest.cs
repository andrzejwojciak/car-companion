using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.Security.Requests
{
    public class AuthWithFacebookRequest
    {
        [Required]
        public string AccessToken { get; set; }        
    }
}