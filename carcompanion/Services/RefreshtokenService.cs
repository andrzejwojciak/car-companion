using System;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Services.Results;
using carcompanion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Services
{
    public class RefreshtokenService : IRefreshtokenService
    {
        private readonly ApplicationDbContext _context;

        public RefreshtokenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
             await _context.AddAsync(refreshToken);
             return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<RefreshTokenValidationResult> ValidateRefreshTokenAsync(string accessTokenJtiString, string refreshTokenIdString)
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
                return RefreshTokenValidationFailed("Something is wrong with your tokens");
            }

            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(i => i.RefreshTokenId == refreshTokenId);

            if(refreshToken == null)
                return RefreshTokenValidationFailed("This refresh token doesn't exist");

            if(refreshToken.AccessTokenJti != accessTokenJti)
                return RefreshTokenValidationFailed("This refresh token isn't related with this access token");

            if(refreshToken.ExpirationDate < DateTime.UtcNow)
                return RefreshTokenValidationFailed("This refresh token is expired");
            
            if(refreshToken.Used)
                return RefreshTokenValidationFailed("This refresh token has been used");

            refreshToken.Used = true;
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();

            return new RefreshTokenValidationResult{ Success = true };
        }

        private RefreshTokenValidationResult RefreshTokenValidationFailed(string errorMessage)
        {
            return new RefreshTokenValidationResult{ ErrorMessage = errorMessage, Success = false };
        }
    }
}