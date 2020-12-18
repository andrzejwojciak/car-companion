using carcompanion.Contract.V1.Responses.Interfaces;

namespace carcompanion.Contract.V1.Responses.Expense
{
    public class DeleteExpenseResponse : IResponseData
    {
        public string Message { get; set; }
    }
}