using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCarAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteCarAsync(Car car)
        {
            _context.Cars.Remove(car);
            _context.UserCars.RemoveRange(car.UserCars);
            return await SaveChangesAsync();
        }

        public async Task<Car> GetCarByIdAsync(Guid carId)
        {
            var car = await _context.Cars
                .Include(e => e.Expenses)
                .Include(u => u.UserCars)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(i => i.CarId == carId);

            return car;
        }

        public async Task<IEnumerable<Car>> GetCarsByUserIdAsync(Guid userId)
        {
            var cars = await _context.Cars
                .Include(u => u.UserCars)
                .ThenInclude(u => u.User)
                .Where(u => u.UserCars.Any(userCar => userCar.UserId == userId))
                .ToListAsync();

            return cars;
        }

        public async Task<bool> UpdateCarAsync(Car car)
        {
            _context.Cars.Update(car);
            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}