using System;
using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenByIdAsync(Guid refreshTokenId);
    }
}