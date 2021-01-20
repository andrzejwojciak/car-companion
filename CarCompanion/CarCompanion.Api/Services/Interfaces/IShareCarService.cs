using System;
using System.Threading.Tasks;
using carcompanion.Results;

namespace carcompanion.Services.Interfaces
{
    public interface IShareCarService
    {
        Task<ServiceResult> CreateShareKeyAsync(Guid carId, Guid userId, string coOwnerRole);
        Task<ServiceResult> UseShareKeyAsync(Guid shareKeyId, Guid userId);
    }
}