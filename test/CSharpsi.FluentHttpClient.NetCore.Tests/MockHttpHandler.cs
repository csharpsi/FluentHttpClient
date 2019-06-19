using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    public class MockHttpHandler : DelegatingHandler
    {
        private readonly Uri baseAddress;
        private readonly Dictionary<(HttpMethod, Uri), HttpResponseMessage> requestCache;
        public Stack<HttpRequestMessage> RequestStack { get; }

        public MockHttpHandler(Uri baseAddress)
        {
            this.baseAddress = baseAddress;
            requestCache = new Dictionary<(HttpMethod, Uri), HttpResponseMessage>();
            RequestStack = new Stack<HttpRequestMessage>();
        }

        public void AddJsonResponse<T>(HttpMethod method, string url, HttpStatusCode statusCode, T body)
        {
            requestCache.Add((method, new Uri(baseAddress, url)), new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
            });
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!requestCache.TryGetValue((request.Method, request.RequestUri), out var response))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            RequestStack.Push(request);

            return Task.FromResult(response);
        }
    }
}
