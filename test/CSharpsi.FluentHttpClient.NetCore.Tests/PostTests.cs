using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    public class PostTests : AbstractHttpExtensionTest
    {
        public PostTests()
        {
            Handler.AddJsonResponse(HttpMethod.Post, "/api/values", HttpStatusCode.OK, new { values = new[] { "1", "2", "3" } });
        }

        [Fact]
        public async Task TestPost()
        {
            var response = await Client
                .Post("/api/values")
                .SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);            
        }

        [Fact]
        public async Task TestPost_WithBody()
        {
            var response = await Client
                .Post("/api/values", new { Test = "body" })
                .SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();

            var expectedBody = JsonConvert.SerializeObject(new { values = new[] { "1", "2", "3" } });

            Assert.Equal(expectedBody, body);

            var request = Handler.RequestStack.Pop();

            Assert.Equal(JsonConvert.SerializeObject(new { Test = "body" }), await request.Content.ReadAsStringAsync());
        }

        
    }
}
