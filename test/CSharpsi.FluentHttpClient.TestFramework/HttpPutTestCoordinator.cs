using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.TestFramework
{
    public class HttpPutTestCoordinator : AbstractTestBase
    {
        public HttpPutTestCoordinator(IAssertion assertion) : base(assertion)
        {
            Handler.AddJsonResponse(HttpMethod.Put, "/api/values", HttpStatusCode.OK, new { values = new[] { "1", "2", "3" } });
        }

        public async Task TestPut()
        {
            var response = await Client
                .Put("/api/values")
                .SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();

            var expectedBody = JsonConvert.SerializeObject(new { values = new[] { "1", "2", "3" } });

            Assert.Equal(expectedBody, body);
        }

        public async Task TestPut_WithBody()
        {
            var response = await Client
                .Put("/api/values", new { Test = "body" })
                .SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();

            var expectedBody = JsonConvert.SerializeObject(new { values = new[] { "1", "2", "3" } });

            Assert.Equal(expectedBody, body);

            Assert.Equal(JsonConvert.SerializeObject(new { Test = "body" }), await Handler.PopRequestString());
        }
    }
}
