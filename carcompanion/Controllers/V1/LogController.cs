using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using carcompanion.Contract.V1;
using carcompanion.Contract.V1.Requests.Log;
using carcompanion.Extensions;
using carcompanion.Models;
using carcompanion.Results;
using carcompanion.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace carcompanion.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [Authorize(Policy = "RequireGetLogPrivilege")]
        [HttpGet(ApiRoutes.Log.GetAllLogs)]
        public async Task<IActionResult> GetAllLogs([FromQuery] GetAllLogsQueryRequest queryRequest)
        {
            var result = await _logService.GetAllLogsAsync(queryRequest.PerPage, queryRequest.Page,
                queryRequest.StartDate,
                queryRequest.EndDate, queryRequest.SortOrder);

            return GenerateResponse(result);
        }

        [HttpGet(ApiRoutes.Log.GetUserLogs)]
        public async Task<IActionResult> GetLogsByUserId([FromQuery] GetAllLogsQueryRequest queryRequest)
        {
            var result = await _logService.GetUserLogsAsync(HttpContext.GetUserId(), queryRequest.PerPage,
                queryRequest.Page,
                queryRequest.StartDate, queryRequest.EndDate, queryRequest.SortOrder);

            return GenerateResponse(result);
        }

        private ActionResult GenerateResponse(ServiceResult result)
            => result.Success
                ? StatusCode(result.Status, result.ResponseData)
                : StatusCode(result.Status, result.ErrorMessage);
    }
}