using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using carcompanion.Services.Interfaces;
using static carcompanion.Contract.V1.ApiRoutes;
using carcompanion.Contract.V1.Requests.Car;
using carcompanion.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using carcompanion.Extensions;
using carcompanion.Results;

namespace carcompanion.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICarService _carService;

        public CarController(IMapper mapper, ICarService carService)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpPost(Cars.CreateCar)]
        public async Task<ActionResult> Create(CreateCarRequest request)
        {
            var result = await _carService.CreateCarAsync(HttpContext.GetUserId(), _mapper.Map<Car>(request));
            return GenerateResponse(result);
        }

        [HttpGet(Cars.GetUserCars)]
        public async Task<ActionResult> GetUserCars()
        {
            var result = await _carService.GetCarsByUserIdAsync(HttpContext.GetUserId());
            return GenerateResponse(result);
        }

        [HttpGet(Cars.GetCarById)]
        public async Task<ActionResult> GetCar([FromRoute] Guid carId)
        {
            var result = await _carService.GetCarByIdAsync(HttpContext.GetUserId(), carId);
            return GenerateResponse(result);
        }

        [HttpPut(Cars.PutCar)]
        public async Task<ActionResult> PutCar([FromRoute] Guid carId, [FromBody] PutCarRequest request)
        {
            var result = await _carService.UpdateCarByIdAsync(HttpContext.GetUserId(), carId, request);
            return GenerateResponse(result);
        }

        [HttpPatch(Cars.PatchCar)]
        public async Task<ActionResult> PatchCar([FromRoute] Guid carId, [FromBody] PatchCarRequest request)
        {
            var result = await _carService.UpdateCarByIdAsync(HttpContext.GetUserId(), carId, request);
            return GenerateResponse(result);
        }

        [HttpDelete(Cars.DeleteCar)]
        public async Task<IActionResult> DeleteCar([FromRoute] Guid carId)
        {
            var result = await _carService.DeleteCarByIdAsync(HttpContext.GetUserId(), carId);
            return GenerateResponse(result);
        }

        private ActionResult GenerateResponse(ServiceResult result) =>
            result.Success
                ? StatusCode(result.Status, result.ResponseData)
                : StatusCode(result.Status, result.ErrorMessage);
    }
}