using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Baddy.Interfaces;
using System.Xml;
using System.IO;
using HtmlAgilityPack;
using Baddy.Helpers;
using Baddy.Models;
using System.Net;

namespace Baddy.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _client;

        public HttpService()
        {
            _client = new HttpClient();
        }

        public async Task<XmlDocument> GetXml(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await _client.SendAsync(request);

                var stringResponse = await response.Content.ReadAsStringAsync();

                var scriptIndex = stringResponse.IndexOf("<script>");
                stringResponse = stringResponse.Substring(0, scriptIndex - 1).Replace("\n", string.Empty);

                var ms = new MemoryStream();
                var xml = XmlWriter.Create(ms);

                var doc = new HtmlDocument
                {
                    OptionOutputAsXml = true,
                };
                doc.LoadHtml(stringResponse);
                doc.Save(xml);

                ms.Position = 0;

                var sr = new StreamReader(ms);
                var parsedResponse = sr.ReadToEnd();

                Console.WriteLine(parsedResponse);

                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(parsedResponse);

                return xmlDoc;
            }
            catch (Exception ex)
            {
                throw new HttpException(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public async Task<T> Post<T>(IEnumerable<KeyValuePair<string, string>> parameters, string url)
        {
            var request = PreparePostMessage(parameters, url);

            var response = await _client.SendAsync(request);

            return await HandleResponse<T>(response);
        }

        public async Task<HttpResponseMessage> Post(IEnumerable<KeyValuePair<string, string>> parameters, string url)
        {
            var request = PreparePostMessage(parameters, url);

            return await _client.SendAsync(request);
        }

        public async Task<bool> PostXml(IEnumerable<KeyValuePair<string, string>> parameters, string url)
        {
            try
            {
                var request = PreparePostMessage(parameters, url);

                var response = await _client.SendAsync(request);

                var stringResponse = await response.Content.ReadAsStringAsync();

                var ms = new MemoryStream();
                var xml = XmlWriter.Create(ms);

                var doc = new HtmlDocument
                {
                    OptionOutputAsXml = true,
                };
                doc.LoadHtml(stringResponse);
                doc.Save(xml);

                return true;
            } 
            catch
            {
                return false;
            }
        }

        private HttpRequestMessage PreparePostMessage(IEnumerable<KeyValuePair<string, string>> parameters, string url)
        {
            var content = new FormUrlEncodedContent(parameters);

            return new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);

            if (response.IsSuccessStatusCode)
                return JsonSerializerHelper.GetObject<T>(content);
            else
                throw new HttpException(response.StatusCode, JsonSerializerHelper.GetString(content));
        }
    }
}
