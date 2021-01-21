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
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private GetUserCarsResponse _getUserCarsResponse;
        private HttpResponseMessage _response;
        private const string Url = "http://localhost:8080/api/v1/cars";
        
        public CarService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        
        public async Task<GetUserCarsResponse> GetUserCars()
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");
            
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Url),
                Headers =
                {
                    {"Authorization", $"Bearer {accessToken}"},
                    {"Accept", "application/json"}
                }
            };
        
            _response = await _httpClient.SendAsync(httpRequestMessage);

            if (_response.IsSuccessStatusCode)
            {
                return await _response.Content.ReadFromJsonAsync<GetUserCarsResponse>();
            }

            return null;
        }
        
    }
}