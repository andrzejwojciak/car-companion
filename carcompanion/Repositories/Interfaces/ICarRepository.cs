using System;
using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Repositories.Interfaces
{
    public interface ICarRepository
    {
        
         Task<bool> CreateCarAsync(Car car);
         Task<Car> GetCarByIdAsync(Guid carId);
         Task<bool> UpdateCarAsync(Car car);
         Task<bool> DeleteCarAsync(Car car);

    }
}