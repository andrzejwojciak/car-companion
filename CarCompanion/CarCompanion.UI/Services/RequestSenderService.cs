using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarCompanion.UI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using CarCompanion.Shared.Results;

namespace CarCompanion.UI.Services
{
    public class RequestSenderService : IRequestSenderService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;

        public RequestSenderService(HttpClient httpClient, NavigationManager navigationManager,
            ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<T>> SendAuthDeleteRequestAsync<T>(string uri)
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Headers =
                {
                    {"Authorization", $"Bearer {accessToken}"},
                    {"Accept", "application/json"}
                }
            };
            
            return await SendRequest<T>(httpRequestMessage);
        }

        public async Task<ServiceResult<T>> SendAuthPostRequestAsync<T>(string uri, object value)
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
            
            return await SendRequest<T>(httpRequestMessage);
        }

        public async Task<ServiceResult<T>> SendAuthGetRequestAsync<T>(string uri)
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

            return await SendRequest<T>(httpRequestMessage);
        }

        private async Task<ServiceResult<T>> SendRequest<T>(HttpRequestMessage httpRequestMessage)
        {
            HttpResponseMessage response;
            
            try
            {
                response = await _httpClient.SendAsync(httpRequestMessage);    
            }
            catch
            {
                return new ServiceResult<T> {Success = false};
            }

            if (!response.IsSuccessStatusCode)
            {
                _navigationManager.NavigateTo("logout");
                return default;
            }

            var data = await response.Content.ReadFromJsonAsync<T>();
            return new ServiceResult<T> {Success = true, Data = data};
        }

    }
}