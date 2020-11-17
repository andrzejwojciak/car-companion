using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using carcompanion.Services.Interfaces;
using static carcompanion.Contract.V1.ApiRoutes;
using carcompanion.Contract.V1.Requests;
using carcompanion.Models;
using carcompanion.Contract.V1.Responses;
using System;

namespace carcompanion.Controllers.V1
{
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

        [HttpPost(Cars.Create)]
        public async Task<ActionResult> CreateAsync(CreateCarRequest request)
        {
            var carModel = _mapper.Map<Car>(request);
            var created = await _carService.CreateCarAsync(carModel);

            if(created)
                return Ok(_mapper.Map<CreateCarResponse>(carModel));
                
            return BadRequest();
        }

        [HttpGet(Cars.GetById)]
        public async Task<ActionResult> GetCarByIdAsync([FromRoute] Guid carId)
        { 
            var carModel = await _carService.GetCarByIdAsync(carId);

            if(carModel == null)
                return BadRequest("Something went wrong!");

            return Ok(_mapper.Map<GetCarByIdResponse>(carModel));
        }

    }
}