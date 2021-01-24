using System.Threading.Tasks;
using CarCompanion.Shared.Contract.V1.Responses.Car;
using CarCompanion.Shared.Results;

namespace CarCompanion.UI.Services.Interfaces
{
    public interface ICarService
    {
        Task<ServiceResult<GetUserCarsResponse>> GetUserCarsAsync();
        Task<ServiceResult<GetCarByIdResponse>> GetCarByIdAsync(string carId);
    }
}