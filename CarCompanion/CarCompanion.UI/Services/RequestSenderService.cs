using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarCompanion.UI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Net;
using System;

namespace CarCompanion.UI.Services
{
    public class RequestSenderService : IRequestSenderService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;

        public RequestSenderService(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
            _httpClient = httpClient;
        }

        public async Task<T> AuthenticateGetRequestAsync<T>(string uri)
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
                {
                    {"Authorization", $"Bearer {accessToken}"},
                    {"Accept", "application/json"}
                }
            };
            
            var response = await _httpClient.SendAsync(httpRequestMessage);
            
            if (!response.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> AuthenticatePostRequestAsync<T>(string uri, object value)
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");
            
            var jsonContent = JsonSerializer.Serialize(value);

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri),
                Headers =
                {

                    {"Authorization", $"Bearer {accessToken}"},
                    {"Accept", "application/json"}
                },
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };
            
            var response = await _httpClient.SendAsync(httpRequestMessage);
            
            if (!response.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}