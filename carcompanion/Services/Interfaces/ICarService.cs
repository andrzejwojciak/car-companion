using carcompanion.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface ICarService
    {       
        Task<IEnumerable<UserCar>> GetUserCarsAsync(string userId);
        Task<bool> CreateCarAsync(Car carModel, string userId);  
        Task<Car> GetCarByIdAsync(Guid carId);
        Task<Car> GetCarWithExpesnesByIdAsync(Guid carId);
    }
}