using System;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            return await SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenByIdAsync(Guid refreshTokenId)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(i => i.RefreshTokenId == refreshTokenId);
            return refreshToken;
        }

        public async Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            return await SaveChangesAsync();
        }

        public async Task<bool> RemoveRefreshTokenAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}