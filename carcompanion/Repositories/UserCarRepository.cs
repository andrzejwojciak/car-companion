using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;

namespace carcompanion.Repositories
{
    public class UserCarRepository : IUserCarRepository
    {
        private readonly ApplicationDbContext _context;

        public UserCarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserCarAsync(UserCar userCar)
        {
            await _context.UserCars.AddAsync(userCar);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteUserCarAsync(UserCar userCar)
        {
            _context.UserCars.Remove(userCar);
            return await SaveChangesAsync();
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}