using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> RegisterUserAsync(User newUser);
        Task<bool> UserExistsByEmailAsync(string email);
        bool IsPasswordMatch(User user, string password);
    }
}