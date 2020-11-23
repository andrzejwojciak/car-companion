using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCarAsync(Car carModel)
        {
            if(carModel.MainName == null)
                GenerateMainName(carModel);

            _context.Cars.Add(carModel);

            var added = await _context.SaveChangesAsync();
            return added > 0 ? true : false;
        }

        public async Task<Car> GetCarByIdAsync(Guid carId) 
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.CarId == carId);
            return car;
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