using System;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using carcompanion.Repositories.Interfaces;
using carcompanion.Results;

namespace carcompanion.Services
{
    public class RefreshtokenService : IRefreshtokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshtokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<bool> SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            return await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);
        }

        public async Task<AuthenticationResult> MakeRefreshTokenUsedAsync(Guid refreshTokenId, Guid accessTokenJti)
        {
            var (result, refreshToken) = await ValidateRefreshTokenAsync(refreshTokenId, accessTokenJti);

            if (!result.Success)
                return result;

            refreshToken.Used = true;
            
            if (!await _refreshTokenRepository.UpdateRefreshTokenAsync(refreshToken))
                return ValidationFailed("Something went wrong");

            return result;
        }

        public async Task<AuthenticationResult> RemoveRefreshTokenAsync(Guid refreshTokenId, Guid accessTokenJti)
        {
            var (result, refreshToken) = await ValidateRefreshTokenAsync(refreshTokenId, accessTokenJti);

            if (!result.Success)
                return result;

            if (!await _refreshTokenRepository.RemoveRefreshTokenAsync(refreshToken))
                return ValidationFailed("Something went wrong");

            return result;
        }

        private async Task<(AuthenticationResult result, RefreshToken refreshToken)> ValidateRefreshTokenAsync(
            Guid refreshTokenId, Guid accessTokenJti)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenByIdAsync(refreshTokenId);

            if (refreshToken == null)
                return (ValidationFailed("This refresh token doesn't exist"), null);

            if (refreshToken.AccessTokenJti != accessTokenJti)
                return (ValidationFailed("This refresh token isn't related with this access token"), null);

            if (refreshToken.ExpirationDate < DateTime.UtcNow)
                return (ValidationFailed("This refresh token is expired"), null);

            if (refreshToken.Used)
                return (ValidationFailed("This refresh token has been used"), null);

            return (new AuthenticationResult {Success = true}, refreshToken);
        }

        private static AuthenticationResult ValidationFailed(string errorMessage)
        {
            return new AuthenticationResult {ErrorMessage = errorMessage, Success = false};
        }
    }
}