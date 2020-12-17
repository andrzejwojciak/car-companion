using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using carcompanion.Services.Interfaces;
using static carcompanion.Contract.V1.ApiRoutes;
using carcompanion.Contract.V1.Requests;
using carcompanion.Models;
using carcompanion.Contract.V1.Responses;
using System;
using Microsoft.AspNetCore.Authorization;
using carcompanion.Extensions;
using System.Linq;
using System.Collections.Generic;

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
            var carModel = _mapper.Map<Car>(request);
            var created = await _carService.CreateCarAsync(carModel, HttpContext.GetUserId());

            if(created)
                return Ok(_mapper.Map<CreateCarResponse>(carModel));
                
            return BadRequest();
        }

        [HttpGet(Cars.GetCarById)]
        public async Task<ActionResult> GetCarById([FromRoute] Guid carId)
        { 
            var car = await _carService.GetUserCarByIdAsync(HttpContext.GetUserId(), carId);

            if(car == null)
                return BadRequest( new { ErrorMessage = "User doesn't have that car or that car doesn't exist"});

            return Ok(_mapper.Map<GetCarByIdResponse>(car));
        }

        [HttpPatch(Cars.PatchCar)]
        public async Task<ActionResult> PatchCar([FromRoute] Guid carId, [FromBody] PatchCarRequest request)
        {
            var car = await _carService.GetUserCarByIdAsync(HttpContext.GetUserId(), carId);

            if(car == null)
                return BadRequest( new { ErrorMessage = "User doesn't have that car or that car doesn't exist"});

            _mapper.Map(request, car);
            var updateSuccess = await _carService.UpdateCarAsync(car);
            
            if(!updateSuccess)
                return BadRequest();

            return Ok(_mapper.Map<GetCarByIdResponse>(car));
        }

        [HttpPut(Cars.PutCar)]
        public async Task<ActionResult> PutCar([FromRoute] Guid carId, [FromBody] PutCarRequest request)
        {
            var car = await _carService.GetUserCarByIdAsync(HttpContext.GetUserId(), carId);

            if(car == null)
                return BadRequest( new { ErrorMessage = "User doesn't have that car or that car doesn't exist"});

            _mapper.Map(request, car);
            var updateSuccess = await _carService.UpdateCarAsync(car);
            
            if(!updateSuccess)
                return BadRequest();

            return Ok(_mapper.Map<GetCarByIdResponse>(car));
        }
        
        [HttpDelete(Cars.DeleteCar)]
        public async Task<IActionResult> DeleteCar([FromRoute] Guid carId)
        {
            
            var car = await _carService.GetUserCarByIdAsync(HttpContext.GetUserId(), carId);

            if(car == null)
                return BadRequest( new { ErrorMessage = "User doesn't have that car or that car doesn't exist"});

            var deleted = await _carService.DeleteCarAwait(car);

            if(!deleted)
                return BadRequest();

            return Ok("Deleted");
        }

        [HttpGet(Cars.GetUserCars)]
        public async Task<ActionResult> GetUserCars()
        {
            var userId = HttpContext.GetUserId();
            var userCars = await _carService.GetUserCarsAsync(userId);
            var cars = userCars.Select(x => x.Car).ToList();

            if(cars.Count() == 0)
                return NotFound( new {ErrorMessage = "User doesn't have any cars"});
            
            var carsResponse = _mapper.Map<IEnumerable<GetCarByIdResponse>>(cars);
            return Ok(new GetUserCarsResponse{ UserId = userId.ToString(), Cars = carsResponse });
        }

    }
}