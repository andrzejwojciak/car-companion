using System.Threading.Tasks;
using carcompanion.Models;

namespace carcompanion.Repositories.Interfaces
{
    public interface IUserCarRepository
    {
        Task<bool> CreateUserCarAsync(UserCar userCar);
        Task<bool> DeleteUserCarAsync(UserCar userCar);
    }
}