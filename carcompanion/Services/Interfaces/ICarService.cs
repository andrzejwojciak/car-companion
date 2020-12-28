using carcompanion.Contract.V1.Requests.Car;
using carcompanion.Models;
using carcompanion.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace carcompanion.Services.Interfaces
{
    public interface ICarService
    {       
        Task<ServiceResult> CreateCarAsync(Guid userId, Car car);
        Task<ServiceResult> GetCarsByUserIdAsync(Guid userId);
        Task<ServiceResult> GetCarByIdAsync(Guid userId, Guid carId);
        Task<ServiceResult> UpdateCarByIdAsync(Guid userId, Guid carId, IUpdateCarRequest request);
        Task<ServiceResult> DeleteCarByIdAsync(Guid userId, Guid carId);
    }
}