using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient
{
    public static class HttpExtensions
    {
        public static HttpRequestMessage AddHeader(this HttpRequestMessage request, string key, params string[] values)
        {
            Check.NotNull(request, nameof(request));
            Check.NotNull(key, nameof(key));
            Check.AtLeastOne(values, nameof(values));

            request.Headers.Add(key, values);
            return request;
        }

        public static HttpRequestMessage AddBearerToken(this HttpRequestMessage request, string token)
        {
            Check.NotNull(request, nameof(request));
            Check.NotNull(token, nameof(token));

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return request;
        }

        public static HttpRequestMessage AddBasicAuth(this HttpRequestMessage request, string username, string password)
        {
            Check.NotNull(request, nameof(request));
            Check.NotNull(username, nameof(username));
            Check.NotNull(password, nameof(password));

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
            return request;
        }

        public static HttpRequestMessage AddJson<T>(this HttpRequestMessage request, T body) where T : class => request.AddJson(body, new JsonSerializerSettings());

        public static HttpRequestMessage AddJson<T>(this HttpRequestMessage request, T body, JsonSerializerSettings settings) where T : class
        {            
            Check.NotNull(request, nameof(request));
            Check.NotNull(body, nameof(body));
            Check.NotNull(settings, nameof(settings));

            request.Content = new StringContent(JsonConvert.SerializeObject(body, settings), Encoding.UTF8, "application/json");
            return request;
        }

        public static HttpRequestMessage AddStream(this HttpRequestMessage request, Stream requestStream, string contentType, int? bufferSize = null)
        {
            Check.NotNull(request, nameof(request));
            Check.NotNull(requestStream, nameof(requestStream));
            Check.NotNull(contentType, nameof(contentType));

            request.Content = bufferSize.HasValue ? new StreamContent(requestStream, bufferSize.Value) : new StreamContent(requestStream);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return request;
        }

        public static async Task<HttpResponseMessage> SendAsync(this HttpRequestMessage request)
        {
            if (!request.Properties.ContainsKey(HttpClientExtensions.HttpClientPropertyKey))
            {
                throw new InvalidOperationException($"The given {nameof(HttpRequestMessage)} has not been configured to use this method. Use Fluent Extensions to configure the request.");
            }

            var client = (HttpClient)request.Properties[HttpClientExtensions.HttpClientPropertyKey];
            request.Properties.Remove(HttpClientExtensions.HttpClientPropertyKey);
            return await client.SendAsync(request).ConfigureAwait(false);
        }       
    }
}
