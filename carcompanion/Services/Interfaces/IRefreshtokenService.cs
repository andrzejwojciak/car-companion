using System.Threading.Tasks;
using carcompanion.Models;
using carcompanion.Results;

namespace carcompanion.Services.Interfaces
{
    public interface IRefreshtokenService
    {
        Task<bool> SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task<AuthenticationResult> ValidateRefreshTokenAsync(string refreshTokenString, string accessTokenJtiString);
    }
}