using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarCompanion.Shared.Contract.Security.Requests;
using CarCompanion.Shared.Contract.Security.Responses;
using CarCompanion.UI.Services.Interfaces;

namespace CarCompanion.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:8080/api/auth-manager/login";

        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<AuthSuccessResponse> LoginAsync(string email, string password)
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            var jsonContent = JsonSerializer.Serialize(loginRequest);


            var response = await _httpClient.PostAsync(ApiUrl,
                new StringContent(jsonContent, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return new AuthSuccessResponse {Success = false};

            var loginResponse = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();

            await _localStorageService.SetItem("accessToken", loginResponse.AccessToken);
            await _localStorageService.SetItem("refreshToken", loginResponse.RefreshToken);
            await _localStorageService.SetItem("email", email);

            return loginResponse;
        }

        public async Task<bool> IsAuthorizedAsync()
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");
            return !string.IsNullOrEmpty(accessToken);
        }

        public async Task LogoutAsync()
        {
            await _localStorageService.RemoveItem("accessToken");
            await _localStorageService.RemoveItem("refreshToken");
            await _localStorageService.RemoveItem("email");
        }
    }
}