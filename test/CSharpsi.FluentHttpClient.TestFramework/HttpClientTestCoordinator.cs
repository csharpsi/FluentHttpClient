using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.TestFramework
{
    public class HttpClientTestCoordinator : AbstractTestBase
    {
        public HttpClientTestCoordinator(IAssertion assertion) : base(assertion)
        {
            Handler.AddJsonResponse(HttpMethod.Get, "/api/values", HttpStatusCode.OK, new { });
        }

        public async Task TestAddHeader()
        {
            var headerValue = Guid.NewGuid().ToString();

            var response = await Client
                .Get("/api/values")
                .AddHeader("Etag", headerValue)
                .SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var request = Handler.RequestStack.Pop();

            request.Headers.TryGetValues("Etag", out var values);

            var first = Assert.Single(values);
            Assert.Equal(headerValue, first);
        }

        public async Task TestAddBasicAuth()
        {
            var response = await Client
                .Get("/api/values")
                .AddBasicAuth("user", "pass")
                .SendAsync();

            var expectedHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes("user:pass"));

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var request = Handler.RequestStack.Pop();

            Assert.Equal("Basic", request.Headers.Authorization.Scheme);
            Assert.Equal(expectedHeaderValue, request.Headers.Authorization.Parameter);
        }

        public async Task TestAddBearerToken()
        {
            var response = await Client
                .Get("/api/values")
                .AddBearerToken("SOMETOKEN")
                .SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var request = Handler.RequestStack.Pop();

            Assert.Equal("Bearer", request.Headers.Authorization.Scheme);
            Assert.Equal("SOMETOKEN", request.Headers.Authorization.Parameter);
        }

        public async Task TestAddJson_WithSettings()
        {
            var response = await Client
                .Get("/api/values")
                .AddJson(new { Test = "body" }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })
                .SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(JsonConvert.SerializeObject(new { test = "body" }), await Handler.PopRequestString());
        }

        public async Task ExceptionWhenUsingSendAsyncOnNonExtensionRequest()
        {
            var request = new HttpRequestMessage();
            await Assert.ThrowsAsync<InvalidOperationException>(() => request.SendAsync());
        }        
    }
}
