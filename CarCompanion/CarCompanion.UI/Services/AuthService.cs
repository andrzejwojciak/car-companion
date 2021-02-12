using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarCompanion.Shared.Contract.Security.Requests;
using CarCompanion.Shared.Contract.Security.Responses;
using CarCompanion.Shared.Results;
using CarCompanion.UI.Services.Interfaces;

namespace CarCompanion.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IRequestSenderService _requestSenderService;
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:8080/api/auth-manager";

        public AuthService(HttpClient httpClient, ILocalStorageService localStorageService,
            IRequestSenderService requestSenderService)
        {
            _requestSenderService = requestSenderService;
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<AuthSuccessResponse>> LoginAsync(string email, string password)
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            const string uri = ApiUrl + "/login";
            var result =
                await _requestSenderService.SendAuthPostRequestAsync<AuthSuccessResponse>(uri, loginRequest);

            if (!result.Success)
                return new ServiceResult<AuthSuccessResponse> {Success = false};

            SaveDataInLocalStorage(result.Data, email);
            return new ServiceResult<AuthSuccessResponse> {Success = true};
        }
        
        public async Task<ServiceResult<AuthSuccessResponse>> RegisterAsync(RegisterRequest request)
        {
            const string uri = ApiUrl + "/register";
            return await _requestSenderService.SendAuthPostRequestAsync<AuthSuccessResponse>(uri, request);
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

        private async void SaveDataInLocalStorage(AuthSuccessResponse data, string email)
        {            
            await _localStorageService.SetItem("accessToken", data.AccessToken);
            await _localStorageService.SetItem("refreshToken", data.RefreshToken);
            await _localStorageService.SetItem("email", email);            
        }
    }
}