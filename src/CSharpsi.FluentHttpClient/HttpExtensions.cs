using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient
{
    public static class HttpExtensions
    {
        private const string HttpClientPropertyKey = "cssi-http-client";

        public static HttpRequestMessage Get(this HttpClient client, string url) => client.CreateRequest(HttpMethod.Get, url);
        public static HttpRequestMessage Put(this HttpClient client, string url) => client.CreateRequest(HttpMethod.Put, url);
        public static HttpRequestMessage Put<T>(this HttpClient client, string url, T body) where T : class => client.CreateRequest(HttpMethod.Put, url).AddJson(body);
        public static HttpRequestMessage Post(this HttpClient client, string url) => client.CreateRequest(HttpMethod.Post, url);
        public static HttpRequestMessage Post<T>(this HttpClient client, string url, T body) where T : class => client.CreateRequest(HttpMethod.Post, url).AddJson(body);
        public static HttpRequestMessage Delete(this HttpClient client, string url) => client.CreateRequest(HttpMethod.Delete, url);
        public static HttpRequestMessage Patch(this HttpClient client, string url) => client.CreateRequest(new HttpMethod("PATCH"), url);
        public static HttpRequestMessage Options(this HttpClient client, string url) => client.CreateRequest(HttpMethod.Options, url);
        public static HttpRequestMessage Head(this HttpClient client, string url) => client.CreateRequest(HttpMethod.Head, url);
        public static HttpRequestMessage Trace(this HttpClient client, string url) => client.CreateRequest(HttpMethod.Trace, url);

        public static HttpRequestMessage AddHeader(this HttpRequestMessage request, string key, params string[] values)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!values.Any())
            {
                throw new ArgumentException("At least one header value is required", nameof(values));
            }

            request.Headers.Add(key, values);
            return request;
        }

        public static HttpRequestMessage AddBearerToken(this HttpRequestMessage request, string token)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return request;
        }

        public static HttpRequestMessage AddBasicAuth(this HttpRequestMessage request, string username, string password)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
            return request;
        }

        public static HttpRequestMessage AddJson<T>(this HttpRequestMessage request, T body) where T : class => request.AddJson(body, new JsonSerializerSettings());

        public static HttpRequestMessage AddJson<T>(this HttpRequestMessage request, T body, JsonSerializerSettings settings) where T : class
        {            
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if(body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            if(settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            request.Content = new StringContent(JsonConvert.SerializeObject(body, settings), Encoding.UTF8, "application/json");
            return request;
        }

        public static async Task<HttpResponseMessage> SendAsync(this HttpRequestMessage request)
        {
            if (!request.Properties.ContainsKey(HttpClientPropertyKey))
            {
                throw new InvalidOperationException($"The given {nameof(HttpRequestMessage)} has not been configured to use this method. Use Fluent Extensions to configure the request.");
            }

            var client = (HttpClient)request.Properties[HttpClientPropertyKey];
            request.Properties.Remove(HttpClientPropertyKey);
            return await client.SendAsync(request).ConfigureAwait(false);
        }

        private static HttpRequestMessage CreateRequest(this HttpClient client, HttpMethod method, string url) => new HttpRequestMessage(method, url)
        {
            Properties =
            {
                {HttpClientPropertyKey, client}
            }
        };
    }
}
