using carcompanion.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface ICarService
    {       
        Task<Car> GetUserCarByIdAsync(Guid userId, Guid carId);
        Task<Car> GetCarByIdAsync(Guid carId);
        Task<bool> UpdateCarAsync(Car car);
        Task<IEnumerable<UserCar>> GetUserCarsAsync(string userId);
        Task<bool> CreateCarAsync(Car carModel, string userId);  
        Task<Car> GetCarWithExpesnesByIdAsync(Guid carId);
    }
}