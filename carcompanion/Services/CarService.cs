using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace carcompanion.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCarAsync(Car carModel, string userId)
        {
            if(carModel.MainName == null)
                GenerateMainName(carModel);

            _context.Cars.Add(carModel);

            var userCar = new UserCar{ UserId = Guid.Parse(userId), CarId = carModel.CarId };
            
            _context.UserCars.Add(userCar);

            var added = await _context.SaveChangesAsync();
            return added > 0 ? true : false;
        }

        public async Task<Car> GetUserCarByIdAsync(Guid userId, Guid carId)
        {
            var hasUserCar = await _context.UserCars.FirstOrDefaultAsync(x => x.UserId == userId && x.CarId == carId);
            if(hasUserCar == null)
                return null;
            
            var car = await GetCarByIdAsync(carId);

            return car;
        }

        public async Task<Car> GetCarByIdAsync(Guid carId) 
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.CarId == carId);
            return car;
        }

        public async Task<IEnumerable<UserCar>> GetUserCarsAsync(string userIdString)
        {
            var userId = Guid.Parse(userIdString);
            
            var userCars = await _context.UserCars.Where(u => u.UserId == userId).Include(c => c.Car).ToListAsync();      
                        
            return userCars;
        }

        public async Task<bool> UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Car> GetCarWithExpesnesByIdAsync(Guid carId)
        {
            var car = await _context.Cars
                                .Include(x => x.Expenses)
                                .FirstOrDefaultAsync(x => x.CarId == carId);

            return car;
        }       
        private void GenerateMainName(Car carModel)
        {
            carModel.MainName = carModel.Brand + "-" + carModel.Model;
        }
    }
}