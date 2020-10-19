using System.ComponentModel.DataAnnotations;

namespace carcompanion.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
    }
}