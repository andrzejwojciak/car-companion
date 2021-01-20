using System.ComponentModel.DataAnnotations;

namespace CarCompanion.Shared.Contract.Security.Requests
{
    public class AuthWithFacebookRequest
    {
        [Required]
        public string AccessToken { get; set; }        
    }
}