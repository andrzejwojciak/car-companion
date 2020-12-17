using carcompanion.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface ICarService
    {       
        Task<bool> DeleteCarAwait(Car car);
        Task<Car> GetUserCarByIdAsync(Guid userId, Guid carId);
        Task<Car> GetCarByIdAsync(Guid carId);
        Task<bool> UpdateCarAsync(Car car);
        Task<IEnumerable<UserCar>> GetUserCarsAsync(Guid userId);
        Task<bool> CreateCarAsync(Car carModel, Guid userId);  
        Task<Car> GetCarWithExpesnesByIdAsync(Guid carId);
    }
}