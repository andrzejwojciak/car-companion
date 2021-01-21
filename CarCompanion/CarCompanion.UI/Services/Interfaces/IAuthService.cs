using System.Threading.Tasks;
using CarCompanion.Shared.Contract.Security.Responses;

namespace CarCompanion.UI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthSuccessResponse> LoginAsync(string username, string password);
        Task<bool> IsAuthorizedAsync();
        Task LogoutAsync();
    }
}