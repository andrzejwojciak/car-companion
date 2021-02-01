using System.Threading.Tasks;
using CarCompanion.Shared.Results;

namespace CarCompanion.UI.Services.Interfaces
{
    public interface IRequestSenderService
    {
        Task<ServiceResult<T>> SendAuthGetRequestAsync<T>(string uri);
        Task<ServiceResult<T>> SendAuthDeleteRequestAsync<T>(string uri);
        Task<ServiceResult<T>> SendAuthPostRequestAsync<T>(string uri, object value);
    }
}