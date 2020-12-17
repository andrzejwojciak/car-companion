using carcompanion.Results.Interfaces;

namespace carcompanion.Results
{
    public class DeleteExpenseResult : IServiceResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }
}