using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.V1.Requests.ShareCar
{
    public class CreateShareKeyRequest
    {   
        [Required]
        public string Role { get; set; }        
        
    }
}