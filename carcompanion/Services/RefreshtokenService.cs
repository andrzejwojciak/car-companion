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

        public async Task<AuthenticationResult> ValidateRefreshTokenAsync(string accessTokenJtiString, string refreshTokenIdString)
        {
            var refreshTokenId = new Guid();
            var accessTokenJti = new Guid();

            try
            {
                refreshTokenId = Guid.Parse(refreshTokenIdString);                
                accessTokenJti = Guid.Parse(accessTokenJtiString);
            }
            catch
            {
                return ValidationFailed("Something is wrong with your tokens");
            }

            var refreshToken = await _refreshTokenRepository.GetRefreshTokenByIdAsync(refreshTokenId);

            if(refreshToken == null)
                return ValidationFailed("This refresh token doesn't exist");

            if(refreshToken.AccessTokenJti != accessTokenJti)
                return ValidationFailed("This refresh token isn't related with this access token");

            if(refreshToken.ExpirationDate < DateTime.UtcNow)
                return ValidationFailed("This refresh token is expired");
            
            if(refreshToken.Used)
                return ValidationFailed("This refresh token has been used");

            refreshToken.Used = true;
            if(!await _refreshTokenRepository.UpdateRefreshTokenAsync(refreshToken))
                return ValidationFailed("Somehting went wrong");

            return new AuthenticationResult{ Success = true };
        }

        private AuthenticationResult ValidationFailed(string errorMessage)
        {
            return new AuthenticationResult{ ErrorMessage = errorMessage, Success = false };
        }
    }
}