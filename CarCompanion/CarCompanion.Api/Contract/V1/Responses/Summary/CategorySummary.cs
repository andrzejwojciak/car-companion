namespace carcompanion.Contract.V1.Responses.Summary
{
    public class CategorySummary
    {
        public string CategoryName { get; set; }
        public decimal CategoryTotalAmount { get; set; }
        public int CategoryExpensesCount { get; set; }
    }
}