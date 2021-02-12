using System.Threading.Tasks;
using CarCompanion.Shared.Contract.Security.Requests;
using CarCompanion.Shared.Contract.Security.Responses;
using CarCompanion.Shared.Results;

namespace CarCompanion.UI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<AuthSuccessResponse>> LoginAsync(string username, string password);
        Task<ServiceResult<AuthSuccessResponse>> RegisterAsync(RegisterRequest request);
        Task<bool> IsAuthorizedAsync();
        Task LogoutAsync();
    }
}