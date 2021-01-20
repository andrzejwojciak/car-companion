using System;
using System.Threading.Tasks;
using carcompanion.Contract.V1.Requests.ShareCar;
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
    public class ShareCarController : ControllerBase
    {
        private readonly IShareCarService _shareCarService;

        public ShareCarController(IShareCarService shareCarService)
        {
            _shareCarService = shareCarService;
        }

        [HttpPost(ShareCar.CreateShareKey)]
        public async Task<IActionResult> CreateShareKey([FromBody] CreateShareKeyRequest request,
            [FromRoute] Guid carId)
        {
            var result = await _shareCarService.CreateShareKeyAsync(carId, HttpContext.GetUserId(), request.Role);
            return GenerateResponse(result);
        }

        [HttpPost(ShareCar.UseShareKey)]
        public async Task<IActionResult> UseShareKey([FromRoute] Guid shareKeyId)
        {
            var result = await _shareCarService.UseShareKeyAsync(HttpContext.GetUserId(), shareKeyId);
            return GenerateResponse(result);
        }

        private ActionResult GenerateResponse(ServiceResult result)
            => !result.Success
                ? StatusCode(result.Status, result.ErrorMessage)
                : StatusCode(result.Status, result.ResponseData);
    }
}