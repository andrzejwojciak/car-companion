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
        private const string CreateShareKeyUrl = "http://localhost:8080/api/v1/cars/{0}/share";
        private const string UseShareKeyUrl = "http://localhost:8080/api/v1/cars/use-sharekey/{0}";
        private readonly IRequestSenderService _requestSenderService;
        
        public ShareKeyService(IRequestSenderService requestSenderService)
        {
            _requestSenderService = requestSenderService;
        }
        
        public async Task<ServiceResult<CreateShareKeyResponse>> GetShareKeyAsync(string carId, CreateShareKeyRequest request)
        {
            var uri = string.Format(CreateShareKeyUrl, carId);
            return await _requestSenderService.SendAuthPostRequestAsync<CreateShareKeyResponse>(uri, request);
        }

        public async Task<ServiceResult<UseShareKeyResponse>> UseShareKeyAsync(string shareKey)
        {
            var uri = string.Format(UseShareKeyUrl, shareKey);
            return await _requestSenderService.SendAuthPostRequestAsync<UseShareKeyResponse>(uri);
        }
    }
}