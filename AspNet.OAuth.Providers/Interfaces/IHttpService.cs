using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNet.OAuth.Providers.Interfaces
{
    public interface IHttpService
    {
        Task<TResult> GetAsync<TResult>(
            string uri,
            Dictionary<string, string> parameters,
            Dictionary<string, string> body,
            Dictionary<string, string> headers,
            string contentType = "application/json");
        Task<TResult> PostAsync<TResult>(
            string uri,
            Dictionary<string, string> parameters,
            Dictionary<string, string> body,
            Dictionary<string, string> headers,
            string contentType = "application/json");
    }
}
