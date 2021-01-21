using System.Threading.Tasks;
using CarCompanion.Shared.Contract.V1.Responses.Car;

namespace CarCompanion.UI.Services.Interfaces
{
    public interface ICarService
    {
        Task<GetUserCarsResponse> GetUserCars();
    }
}