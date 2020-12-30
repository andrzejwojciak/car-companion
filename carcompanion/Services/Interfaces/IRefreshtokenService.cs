using System;
using System.Threading.Tasks;
using carcompanion.Models;
using carcompanion.Results;

namespace carcompanion.Services.Interfaces
{
    public interface IRefreshtokenService
    {
        Task<bool> SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task<AuthenticationResult> MakeRefreshTokenUsedAsync(Guid refreshTokenId, Guid accessTokenJti);
        Task<AuthenticationResult> RemoveRefreshTokenAsync(Guid refreshTokenId, Guid accessTokenJti);
    }
}