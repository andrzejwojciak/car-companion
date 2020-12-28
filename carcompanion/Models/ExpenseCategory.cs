using System.ComponentModel.DataAnnotations;

namespace carcompanion.Models
{
    public class ExpenseCategory
    {
        [Key]
        public string ExpenseCategoryId { get; set; }        
        public string Description { get; set; }
        public string ParentId { get; set; }
    }
}