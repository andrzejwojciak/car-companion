using System.Threading.Tasks;
using carcompanion.Contract.V1.Requests.ShareCar;
using CarCompanion.Shared.Contract.V1.Responses.ShareCar;
using CarCompanion.Shared.Results;

namespace CarCompanion.UI.Services.Interfaces
{
    public interface IShareKeyService
    {
        Task<ServiceResult<CreateShareKeyResponse>> GetShareKeyAsync(string carId, CreateShareKeyRequest request);
    }
}