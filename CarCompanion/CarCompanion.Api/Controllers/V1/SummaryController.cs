using System;
using System.Threading.Tasks;
using carcompanion.Contract.V1.Requests.Summary;
using carcompanion.Extensions;
using carcompanion.Results;
using carcompanion.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static carcompanion.Contract.V1.ApiRoutes;

namespace carcompanion.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet(Summary.GetSummaryForCar)]
        public async Task<IActionResult> GetSummary([FromRoute] Guid carId, [FromQuery] GetSummaryQueryRequest request)
        {
            var result = await _summaryService.GetSummaryByCarIdAsync(carId, HttpContext.GetUserId(), request.StartDate, request.EndDate);
            return GenerateResponse(result);
        }

        private ActionResult GenerateResponse(ServiceResult result) => !result.Success
            ? StatusCode(result.Status, result.ErrorMessage)
            : StatusCode(result.Status, result.ResponseData);
    }
}