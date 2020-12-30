using System;
using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenByIdAsync(Guid refreshTokenId);
        Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<bool> RemoveRefreshTokenAsync(RefreshToken refreshToken);
    }
}