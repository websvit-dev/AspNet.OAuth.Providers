using AspNet.OAuth.Providers.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.OAuth.Providers.Services
{
    public class HttpService : IHttpService
    {
        public async Task<TResult> GetAsync<TResult>(
            string baseUri,
            Dictionary<string, string> parameters,
            Dictionary<string, string> body,
            Dictionary<string, string> headers,
            string contentType = "application/json")
        {
            var content = await SendRequestAsync(HttpMethod.Get, baseUri, parameters, body, headers, contentType);
            return JsonConvert.DeserializeObject<TResult>(content);
        }

        public async Task<TResult> PostAsync<TResult>(
            string baseUri,
            Dictionary<string, string> parameters,
            Dictionary<string, string> body,
            Dictionary<string, string> headers,
            string contentType = "application/json")
        {
            var content = await SendRequestAsync(HttpMethod.Post, baseUri, parameters, body, headers, contentType);
            return JsonConvert.DeserializeObject<TResult>(content);
        }

        private async Task<string> SendRequestAsync(HttpMethod method,
            string baseUri,
            Dictionary<string, string> parameters,
            Dictionary<string, string> body,
            Dictionary<string, string> headers,
            string contentType)
        {
            if (parameters != null && parameters.Count > 0)
            {
                baseUri += "?";
                string param = "";

                foreach (var pair in parameters)
                {
                    param += $"&{pair.Key}={pair.Value}";
                }

                baseUri += param.Substring(1);
            }

            var request = new HttpRequestMessage(method, baseUri);


            if (headers != null && headers.Count > 0)
            {
                foreach (var pair in headers)
                {
                    request.Headers.Add(pair.Key, new List<string>() { pair.Value });
                }
            }

            if (body != null && body.Count > 0)
            {
                HttpContent content = null;

                switch (contentType)
                {
                    case "application/x-www-form-urlencoded":
                        content = new FormUrlEncodedContent(body.ToList());
                        break;
                    default:
                        content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, contentType);
                        break;
                }

                request.Content = content;
            }

            var httpClient = new HttpClient();
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new System.Exception(content);
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
