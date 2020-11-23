using carcompanion.Models;
using System;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface ICarService
    {       
        Task<bool> CreateCarAsync(Car carModel);  
        Task<Car> GetCarByIdAsync(Guid carId);
        Task<Car> GetCarWithExpesnesByIdAsync(Guid carId);
    }
}