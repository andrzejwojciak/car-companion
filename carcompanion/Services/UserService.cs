using System.Linq;
using System.Threading.Tasks;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Security;
using carcompanion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _hasher;

        public UserService(ApplicationDbContext context, IPasswordHasher hasher)
        {
            _hasher = hasher;
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<bool> RegisterUserAsync(User newUser)
        {
            newUser.Password = _hasher.HashPassword(newUser.Password);
            await _context.Users.AddAsync(newUser);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UserExistsByEmailAsync(string email) 
        {
            return await _context.Users.AnyAsync(e => e.Email == email);
        }

        public bool IsPasswordMatch(User user, string password)
        {
            return _hasher.VerifyHashedUserPassword(user.Password, password);
        } 
        
    }
}