using System.Threading.Tasks;
using CarCompanion.Shared.Contract.V1.Requests.Expense;
using CarCompanion.Shared.Contract.V1.Responses.Expense;
using CarCompanion.Shared.Results;

namespace CarCompanion.UI.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<ServiceResult<GetExpensesByCarIdResponse>> GetExpensesByCarIdAsync(string carId);
        Task<ServiceResult<ExpenseResponse>> CreateExpenseAsync(CreateExpenseRequest request, string carId);
        Task<ServiceResult<ExpenseResponse>> UpdateExpenseAsync(PatchExpenseRequest request, string carId, string expenseId);
        Task<ServiceResult<DeleteExpenseResponse>> DeleteExpenseAsync(string carId, string expenseId);
    }
}