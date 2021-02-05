using System;
using System.Threading.Tasks;
using CarCompanion.Shared.Contract.V1.Requests.Expense;
using CarCompanion.Shared.Contract.V1.Responses.Car;
using CarCompanion.Shared.Contract.V1.Responses.Expense;
using CarCompanion.Shared.Results;
using CarCompanion.UI.Services.Interfaces;

namespace CarCompanion.UI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRequestSenderService _requestSenderService;
        private const string Url = "http://localhost:8080/api/v1/cars/{0}/expenses";

        public ExpenseService(IRequestSenderService requestSenderService)
        {
            _requestSenderService = requestSenderService;
        }

        public async Task<ServiceResult<GetExpensesByCarIdResponse>> GetExpensesByCarIdAsync(string carId)
        {
            var uri = string.Format(Url, carId);
            return await _requestSenderService.SendAuthGetRequestAsync<GetExpensesByCarIdResponse>(uri);
        }

        public async Task<ServiceResult<ExpenseResponse>> CreateExpenseAsync(CreateExpenseRequest request, string carId)
        {
            var uri = string.Format(Url, carId);
            return await _requestSenderService.SendAuthPostRequestAsync<ExpenseResponse>(uri, request);
        }
    }
}