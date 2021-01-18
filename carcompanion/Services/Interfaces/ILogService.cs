using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using carcompanion.Models;
using carcompanion.Results;

namespace carcompanion.Services.Interfaces
{
    public interface ILogService
    {
        Task<ServiceResult> GetAllLogsAsync(int perPage, int page, DateTime startDate, DateTime endDate, string order);
        Task<ServiceResult> GetUserLogsAsync(Guid userId, int perPage, int page, DateTime startDate, DateTime endDate, string order);
    }
}