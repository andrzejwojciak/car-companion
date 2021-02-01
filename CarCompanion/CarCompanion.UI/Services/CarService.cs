using System.Threading.Tasks;
using CarCompanion.Shared.Contract.V1.Requests.Car;
using CarCompanion.Shared.Contract.V1.Responses.Car;
using CarCompanion.Shared.Results;
using CarCompanion.UI.Services.Interfaces;

namespace CarCompanion.UI.Services
{
    public class CarService : ICarService
    {
        private const string Url = "http://localhost:8080/api/v1/cars";
        private readonly IRequestSenderService _requestSenderService;

        public CarService(IRequestSenderService requestSenderService)
        {
            _requestSenderService =  requestSenderService;
        }

        public async Task<ServiceResult<GetUserCarsResponse>> GetUserCarsAsync()
        {
            return await _requestSenderService.SendAuthGetRequestAsync<GetUserCarsResponse>(Url);
        }

        public async Task<ServiceResult<GetCarByIdResponse>> GetCarByIdAsync(string carId)
        {
            return await _requestSenderService.SendAuthGetRequestAsync<GetCarByIdResponse>(Url + "/" + carId);
        }

        public async Task<ServiceResult<CreateCarResponse>> CreateCarAsync(CreateCarRequest request)
        {
            return await _requestSenderService.SendAuthPostRequestAsync<CreateCarResponse>(Url, request);
        }

        public async Task<ServiceResult<DeleteCarResponse>> DeleteCarAsync(string carId)
        {
            return await _requestSenderService.SendAuthDeleteRequestAsync<DeleteCarResponse>(Url + "/" + carId);
        }
    }
}