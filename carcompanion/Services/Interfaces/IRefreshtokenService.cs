using System.Threading.Tasks;
using carcompanion.Models;
using carcompanion.Services.Results;

namespace carcompanion.Services.Interfaces
{
    public interface IRefreshtokenService
    {
        Task<bool> SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshTokenValidationResult> ValidateRefreshTokenAsync(string refreshTokenString, string accessTokenJtiString);
    }
}