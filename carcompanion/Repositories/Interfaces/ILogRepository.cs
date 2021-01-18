using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<(IEnumerable<Log>, int)> GetAllLogsAsync(int perPage, int page, DateTime startDate, DateTime endDate, string order);

        Task<(IEnumerable<Log>, int)> GetUserLogsAsync(Guid userId, int perPage, int page, DateTime startDate,
            DateTime endDate, string order);
    }
}