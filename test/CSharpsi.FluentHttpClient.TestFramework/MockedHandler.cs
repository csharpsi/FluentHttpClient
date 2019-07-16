using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.TestFramework
{
    public class MockedHandler : DelegatingHandler
    {
        private readonly Uri baseAddress;
        private readonly Dictionary<(HttpMethod, Uri), HttpResponseMessage> requestCache;

        public Stack<HttpRequestMessage> RequestStack { get; }
        public Stack<MemoryStream> RequestStreamStack { get; }

        public MockedHandler(Uri baseAddress)
        {
            this.baseAddress = baseAddress;
            requestCache = new Dictionary<(HttpMethod, Uri), HttpResponseMessage>();
            RequestStack = new Stack<HttpRequestMessage>();
            RequestStreamStack = new Stack<MemoryStream>();
        }

        public void AddJsonResponse<T>(HttpMethod method, string url, HttpStatusCode statusCode, T body)
        {
            requestCache.Add((method, new Uri(baseAddress, url)), new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
            });
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!requestCache.TryGetValue((request.Method, request.RequestUri), out var response))
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            if(request.Content != null)
            {
                var requestStream = new MemoryStream();
                await request.Content.CopyToAsync(requestStream);
                requestStream.Position = 0;
                RequestStreamStack.Push(requestStream);
            }            

            RequestStack.Push(request);            

            return response;
        }

        public async Task<string> PopRequestString()
        {
            var stream = RequestStreamStack.Pop();
            return await new StreamReader(stream).ReadToEndAsync();
        }
    }
}
