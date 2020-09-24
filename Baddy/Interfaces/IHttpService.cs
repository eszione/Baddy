using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace Baddy.Interfaces
{
    public interface IHttpService
    {
        Task<XmlDocument> GetXml(string url);
        Task<T> Post<T>(IEnumerable<KeyValuePair<string, string>> parameters, string url);
        Task<HttpResponseMessage> Post(IEnumerable<KeyValuePair<string, string>> parameters, string url);
        Task<bool> PostXml(IEnumerable<KeyValuePair<string, string>> parameters, string url);
    }
}
