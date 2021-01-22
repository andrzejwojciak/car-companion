using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CarCompanion.Shared.Contract.V1.Responses.Car;
using CarCompanion.UI.Services.Interfaces;

namespace CarCompanion.UI.Services
{
    public class CarService : ICarService
    {
        private const string Url = "http://localhost:8080/api/v1/cars";
        private readonly IRequestSenderService _requestSenderService;

        public CarService(IRequestSenderService reqestSenderService)
        {
            _requestSenderService =  reqestSenderService;
        }

        public async Task<GetUserCarsResponse> GetUserCarsAsync()
        {
            return await _requestSenderService.AuthenticateGetRequestAsync<GetUserCarsResponse>(Url);
        }
        
    }
}