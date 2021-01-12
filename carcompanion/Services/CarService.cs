using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using carcompanion.Results;
using carcompanion.Repositories.Interfaces;
using carcompanion.Contract.V1.Responses.Interfaces;
using carcompanion.Contract.V1.Responses.Car;
using AutoMapper;
using carcompanion.Contract.V1.Requests.Car;

namespace carcompanion.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICarRepository _carRepository;
        private readonly IUserCarRepository _userCarRepository;
        private readonly IMapper _mapper;

        public CarService(ApplicationDbContext context, ICarRepository carRepository, IUserCarRepository userCarRepository, IMapper mapper)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _userCarRepository = userCarRepository;
            _context = context;
        }

        public async Task<ServiceResult> CreateCarAsync(Guid userId, Car car)
        {   
            if(car.MainName == null)
                GenerateMainName(car);

            if(!await _carRepository.CreateCarAsync(car))
                return FailResult(400, "Couldn't create a car");

            var userCar = new UserCar { UserId = userId, CarId = car.CarId, UserCarRoleId = "owner"};            
            if(!await _userCarRepository.CreateUserCarAsync(userCar))
                return FailResult(400, "Couldn't create a car");            

            return SuccessResult(201, _mapper.Map<CreateCarResponse>(car));
        }

        public async Task<ServiceResult> GetCarsByUserIdAsync(Guid userId)
        {
            var cars = await _carRepository.GetCarsByUserIdAsync(userId);

            if(cars == null)
                return FailResult(404, "User doesn't have any car");
                        
            var carsResponse = new List<GetCarByIdResponse>();  
            
            foreach(var car in cars)
            {
                var carResponse = _mapper.Map<GetCarByIdResponse>(car);
                carResponse.Users = GenerateCarUsersResponse(car);
                carsResponse.Add(carResponse);
            }
            
            var response = new GetUserCarsResponse{ UserId = userId.ToString(), Cars = carsResponse};
            return SuccessResult(200, response);
        }

        public async Task<ServiceResult> GetCarByIdAsync(Guid userId, Guid carId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);

            if(car == null)
                return FailResult(404, "Car doesn't exist");
            
            if(car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)
                return FailResult(401, "Car doesn't belong to this user");
            
            var response = _mapper.Map<GetCarByIdResponse>(car);
            response.Users = GenerateCarUsersResponse(car);

            return SuccessResult(200, response);
        }

        public async Task<ServiceResult> UpdateCarByIdAsync(Guid userId, Guid carId, IUpdateCarRequest request)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);

            if(car == null)
                return FailResult(404, "Car doesn't exist");
            
            if(car.UserCars.FirstOrDefault(u => u.UserId == userId) == null)
                return FailResult(401, "Car doesn't belong to this user");
            
            car = _mapper.Map(request, car);

            if(!await _carRepository.UpdateCarAsync(car))
                return FailResult(500, "Failed while updating the car");

            return SuccessResult(200, _mapper.Map<UpdateCarResponse>(car));
        }
        
        public async Task<ServiceResult> DeleteCarByIdAsync(Guid userId, Guid carId)
        {
            var car = await _carRepository.GetCarByIdAsync(carId);

            if(car == null)
                return FailResult(404, "Car doesn't exist");
            
            var userCar = car.UserCars.FirstOrDefault(u => u.UserId == userId);
            if(userCar == null || !userCar.UserCarRoleId.Equals("Owner"))
                return FailResult(401, "User can't delete this car");
            
            if(!await _carRepository.DeleteCarAsync(car))
                return FailResult(500, "Failed while deleting car");
            
            return SuccessResult(200, new DeleteCarResponse { CarDeleted = true});

        }

        private ICollection<CarUserResponse> GenerateCarUsersResponse(Car car)
        {               
            var userCars = car.UserCars;
            var carUsersResponse = new List<CarUserResponse>();

            foreach(var userCar in userCars)
            {
                carUsersResponse.Add( new CarUserResponse{ 
                    Email = userCar.User.Email, 
                    Role = userCar.UserCarRoleId }
                    );
            }

            return (ICollection<CarUserResponse>)carUsersResponse;
        } 

        private ServiceResult FailResult(int statusCode, string ErrorMessage)
        {
            return new ServiceResult{ Success = false, StatusCode = statusCode, ErrorMessage = ErrorMessage};
        }

        private ServiceResult SuccessResult(int statusCode, IResponseData responseData)
        {
            return new ServiceResult{ Success = true, StatusCode = statusCode, ResponseData = responseData};
        }
    
        private void GenerateMainName(Car car)
        {
            car.MainName = car.Brand + "-" + car.Model;
        }
    }
}