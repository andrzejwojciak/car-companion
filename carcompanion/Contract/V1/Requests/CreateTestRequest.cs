using System.ComponentModel.DataAnnotations;

namespace carcompanion.Contract.V1.Requests
{
    public class CreateTestRequest
    {
        [Required]
        public string Content { get; set; }
    }
}