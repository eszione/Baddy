using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baddy.Interfaces
{
    public interface IHttpService
    {
        Task<T> Get<T>(string url);
        Task<T> Post<T>(IEnumerable<KeyValuePair<string, string>> parameters, string url);
        Task<HttpResponseMessage> Post(IEnumerable<KeyValuePair<string, string>> parameters, string url);
    }
}
