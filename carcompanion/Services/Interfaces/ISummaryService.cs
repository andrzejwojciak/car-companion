using System;
using System.Threading.Tasks;
using carcompanion.Results;

namespace carcompanion.Services.Interfaces
{
    public interface ISummaryService
    {
        Task<ServiceResult> GetSummaryByCarIdAsync(Guid carId, DateTime startDate, DateTime endDate);
    }
}