using System;
using System.Threading.Tasks;
using carcompanion.Contract.V1.Requests.ShareCar;
using CarCompanion.Shared.Contract.V1.Responses.ShareCar;
using CarCompanion.Shared.Results;
using CarCompanion.UI.Services.Interfaces;

namespace CarCompanion.UI.Services
{
    public class ShareKeyService : IShareKeyService
    {
        private const string Url = "http://localhost:8080/api/v1/cars/{0}/share";
        private readonly IRequestSenderService _requestSenderService;
        
        public ShareKeyService(IRequestSenderService requestSenderService)
        {
            _requestSenderService = requestSenderService;
        }
        
        public Task<ServiceResult<CreateShareKeyResponse>> GetShareKeyAsync(string carId, CreateShareKeyRequest request)
        {
            var uri = string.Format(Url, carId);
            return _requestSenderService.SendAuthPostRequestAsync<CreateShareKeyResponse>(uri, request);
        }
    }
}