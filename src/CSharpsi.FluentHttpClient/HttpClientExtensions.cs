using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace CSharpsi.FluentHttpClient
{
    public static class HttpClientExtensions
    {
        public const string HttpClientPropertyKey = "cssi-http-client";

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

        private static HttpRequestMessage CreateRequest(this HttpClient client, HttpMethod method, string url) => new HttpRequestMessage(method, url)
        {
            Properties =
            {
                {HttpClientPropertyKey, client}
            }
        };
    }
}
