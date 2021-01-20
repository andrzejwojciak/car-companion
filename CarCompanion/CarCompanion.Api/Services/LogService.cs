using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using carcompanion.Contract.V1.Responses.Interfaces;
using carcompanion.Contract.V1.Responses.Log;
using carcompanion.Repositories.Interfaces;
using carcompanion.Results;
using carcompanion.Services.Interfaces;

namespace carcompanion.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;

        public LogService(ILogRepository logRepository, IMapper mapper)
        {
            _mapper = mapper;
            _logRepository = logRepository;
        }

        //TODO: clean up this code
        public async Task<ServiceResult> GetAllLogsAsync(int perPage, int page, DateTime startDate, DateTime endDate,
            string order)
        {
            if (startDate > endDate)
                return FailedResult("startDate must by later than endDate", 400);

            switch (order)
            {
                case ("oldest"):
                case ("latest"):
                    break;
                default:
                    return FailedResult("SortOrder  must be 'latest' or 'oldest'", 400);
            }

            var (logs, totalPages) = await _logRepository.GetAllLogsAsync(perPage, page, startDate, endDate, order);

            totalPages = (int) Math.Ceiling((double) totalPages / perPage);

            var response = new GetAllLogsResponse
            {
                Page = page, PerPage = perPage, TotalPages = totalPages,
                Logs = _mapper.Map<IEnumerable<AdminLogResponse>>(logs)
            };

            return SuccessResult(response, 200);
        }

        //TODO: clean up this code
        public async Task<ServiceResult> GetUserLogsAsync(Guid userId, int perPage, int page, DateTime startDate,
            DateTime endDate, string order)
        {
            if (startDate > endDate)
                return FailedResult("StartDate must by later than EndDate ", 400);

            switch (order)
            {
                case ("oldest"):
                case ("latest"):
                    break;
                default:
                    return FailedResult("SortOrder  must be 'latest' or 'oldest'", 400);
            }

            var (logs, totalPages) =
                await _logRepository.GetUserLogsAsync(userId, perPage, page, startDate, endDate, order);

            totalPages = (int) Math.Ceiling((double) totalPages / perPage);

            var response = new GetUserLogsResponse
            {
                Page = page, PerPage = perPage, TotalPages = totalPages,
                Logs = _mapper.Map<IEnumerable<UserLogResponse>>(logs)
            };

            return SuccessResult(response, 200);
        }

        private static ServiceResult FailedResult(string errorMessage, int statusCode)
            => new ServiceResult
                {Success = false, Status = statusCode, ErrorMessage = errorMessage};

        private static ServiceResult SuccessResult(IResponseData responseData, int statusCode)
            => new ServiceResult {Success = true, Status = statusCode, ResponseData = responseData};
    }
}