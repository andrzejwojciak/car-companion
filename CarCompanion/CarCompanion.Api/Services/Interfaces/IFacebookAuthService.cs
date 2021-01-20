using System.Threading.Tasks;
using carcompanion.Results;

namespace carcompanion.Services.Interfaces
{
    public interface IFacebookAuthService
    {
        Task<FacebookAuthResult> AuthUserByFbTokenAsync(string accessToken);
    }
}