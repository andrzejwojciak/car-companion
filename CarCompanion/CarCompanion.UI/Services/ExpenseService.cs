using System.Threading.Tasks;
using CarCompanion.Shared.Contract.V1.Responses.Car;
using CarCompanion.Shared.Contract.V1.Responses.Expense;
using CarCompanion.Shared.Results;
using CarCompanion.UI.Services.Interfaces;

namespace CarCompanion.UI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRequestSenderService _requestSenderService;
        private string _url = "http://localhost:8080/api/v1/cars/{carId}/expenses";
        
        public ExpenseService(IRequestSenderService requestSenderService)
        {
            _requestSenderService = requestSenderService;
        }

        public async Task<ServiceResult<GetExpensesByCarIdResponse>> GetExpensesByCarIdAsync(string carId)
        {
            var uri = $"http://localhost:8080/api/v1/cars/{carId}/expenses";
            return await _requestSenderService.SendAuthGetRequestAsync<GetExpensesByCarIdResponse>(uri);
        }
    }
}