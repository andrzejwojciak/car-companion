using System.Threading.Tasks;

namespace CarCompanion.UI.Services.Interfaces
{
    public interface IRequestSenderService
    {
        Task<T> AuthenticateGetRequestAsync<T>(string uri);
        Task<T> AuthenticatePostRequestAsync<T>(string uri, object value);
         
    }
}