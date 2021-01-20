using System;
using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Repositories.Interfaces
{
    public interface IShareKeyRepository
    {
        Task<bool> CreateShareKeyAsync(ShareKey shareKey);
        Task<ShareKey> GetShareKeyByIdAsync(Guid shareKeyId);
        Task<bool> UpdateShareKeyAsync(ShareKey shareKey);
    }
}