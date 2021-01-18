using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Repositories.Interfaces;
using carcompanion.Models;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext _context;

        public LogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Log>, int)> GetAllLogsAsync(int perPage, int page, DateTime startDate,
            DateTime endDate, string order)
        {
            // var query = _context.Logs
            //     .AsQueryable()
            //     .Where(u => u.Timestamp >= startDate && u.Timestamp <= endDate);

            // switch(order)
            // {
            //     case("latest"):
            //         query.OrderByDescending(u => u.Id);
            //         break;

            //     case("oldest"):
            //         query.OrderBy(u => u.Id);
            //         break;
            // }

            // var resultTask = query
            //     .Skip(perPage * (page - 1))
            //     .Take(perPage)
            //     .ToArrayAsync();

            // var countTask = query.CountAsync();

            // await Task.WhenAll(resultTask, countTask);

            // return await resultTask;
            //TODO: no time to do this no hardcoded :(


            var count = _context.Logs
                .Count(u => u.Timestamp >= startDate && u.Timestamp <= endDate);

            switch (order)
            {
                case ("latest"):

                    var logsLatest = await _context.Logs
                        .OrderByDescending(u => u.Timestamp)
                        .Where(u => u.Timestamp >= startDate && u.Timestamp <= endDate)
                        .Skip(perPage * (page - 1))
                        .Take(perPage)
                        .ToListAsync();

                    return (logsLatest, count);

                case ("oldest"):

                    var logsOldest = await _context.Logs
                        .OrderBy(u => u.Timestamp)
                        .Where(u => u.Timestamp >= startDate && u.Timestamp <= endDate)
                        .Skip(perPage * (page - 1))
                        .Take(perPage)
                        .ToListAsync();

                    return (logsOldest, count);
            }

            return (null, 0);
        }

        //TODO: no time to do this no hardcoded :(
        public async Task<(IEnumerable<Log>, int)> GetUserLogsAsync(Guid userId, int perPage, int page,
            DateTime startDate,
            DateTime endDate, string order)
        {
            var count = _context.Logs
                .Count(u => u.Timestamp >= startDate && u.Timestamp <= endDate && u.UserId == userId.ToString());

            switch (order)
            {
                case ("latest"):

                    var logsLatest = await _context.Logs
                        .OrderByDescending(u => u.Timestamp)
                        .Where(u => u.Timestamp >= startDate && u.Timestamp <= endDate && u.UserId == userId.ToString())
                        .Skip(perPage * (page - 1))
                        .Take(perPage)
                        .ToListAsync();

                    return (logsLatest, count);

                case ("oldest"):

                    var logsOldest = await _context.Logs
                        .OrderBy(u => u.Timestamp)
                        .Where(u => u.Timestamp >= startDate && u.Timestamp <= endDate && u.UserId == userId.ToString())
                        .Skip(perPage * (page - 1))
                        .Take(perPage)
                        .ToListAsync();

                    return (logsOldest, count);
            }

            return (null, 0);
        }
    }
}