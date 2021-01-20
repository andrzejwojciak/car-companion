using System;
using System.Threading.Tasks;
using carcompanion.Results;

namespace carcompanion.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterUserAsync(string email, string password);
        Task<AuthenticationResult> LoginUserAsync(string email, string password);
        Task<AuthenticationResult> AuthWithFacebookAsync(string accessToken);
        Task<AuthenticationResult> RefreshTokenAsync(Guid refreshToken, string accessToken);
        Task<LogoutResult> LogoutUserAsync(Guid refreshToken, Guid accessTokenJti);
    }
}