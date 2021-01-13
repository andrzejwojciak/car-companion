using System;
using System.Threading.Tasks;
using carcompanion.Contract.Security.Requests;
using carcompanion.Results;

namespace carcompanion.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterUserAsync(RegisterRequest request);
        Task<AuthenticationResult> LoginUserAsync(LoginRequest request);
        Task<AuthenticationResult> AuthWithFacebookAsync(AuthWithFacebookRequest request);
        Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request);
        Task<LogoutResult> LogoutUserAsync(Guid refreshToken, Guid accessTokenJti);
    }
}