using System;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Repositories
{
    public class ShareKeyRepository : IShareKeyRepository
    {
        private readonly ApplicationDbContext _context;

        public ShareKeyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateShareKeyAsync(ShareKey shareKey)
        {
            await _context.ShareKeys.AddAsync(shareKey);
            return await SaveChangesAsync();
        }

        public async Task<ShareKey> GetShareKeyByIdAsync(Guid shareKeyId)
        {
            var shareKey = await _context.ShareKeys.FirstOrDefaultAsync(x => x.ShareKeyId == shareKeyId);
            return shareKey;
        }

        public async Task<bool> UpdateShareKeyAsync(ShareKey shareKey)
        {
            _context.ShareKeys.Update(shareKey);
            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}