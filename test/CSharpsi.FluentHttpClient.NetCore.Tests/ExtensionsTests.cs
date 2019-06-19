using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    public class ExtensionsTests : AbstractHttpExtensionTest
    {
        public ExtensionsTests()
        {
            Handler.AddJsonResponse(HttpMethod.Get, "/api/values", HttpStatusCode.OK, new { });
        }

        [Fact]
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

            Assert.Single(values);
            Assert.Equal(headerValue, values.First());
        }

        [Fact]
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

        [Fact]
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

        [Fact]
        public async Task TestAddJson_WithSettings()
        {
            var response = await Client
                .Get("/api/values")
                .AddJson(new { Test = "body" }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })
                .SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var request = Handler.RequestStack.Pop();

            Assert.Equal(JsonConvert.SerializeObject(new { test = "body" }), await request.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task ExceptionWhenUsingSendAsyncOnNonExtensionRequest()
        {
            var request = new HttpRequestMessage();
            await Assert.ThrowsAsync<InvalidOperationException>(() => request.SendAsync());
        }
    }
}
