using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.V1.Requests
{
    public class UpdateTestRequest
    {
        [Required]
        public string Content { get; set; }
    }
}