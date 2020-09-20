using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Baddy.Interfaces;

namespace Baddy.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _client;

        public HttpService()
        {
            _client = new HttpClient();
        }

        public async Task<T> Get<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _client.SendAsync(request);

            var stringResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine(stringResponse);

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        public async Task<T> Post<T>(IEnumerable<KeyValuePair<string, string>> parameters, string url)
        {
            var content = new FormUrlEncodedContent(parameters);

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            var response = await _client.SendAsync(request);

            var stringResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine(stringResponse);

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        public async Task<HttpResponseMessage> Post(IEnumerable<KeyValuePair<string, string>> parameters, string url)
        {
            var content = new FormUrlEncodedContent(parameters);

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };

            var response = await _client.SendAsync(request);

            return response;
        }
    }
}
