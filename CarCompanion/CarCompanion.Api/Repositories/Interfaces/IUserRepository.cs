using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> AddUserAsync(User user);
    }
}