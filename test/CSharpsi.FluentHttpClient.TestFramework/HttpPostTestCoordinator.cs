using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.TestFramework
{
    public class HttpPostTestCoordinator : AbstractTestBase
    {
        public HttpPostTestCoordinator(IAssertion assertion) : base(assertion)
        {
            Handler.AddJsonResponse(HttpMethod.Post, "/api/values", HttpStatusCode.OK, new { values = new[] { "1", "2", "3" } });
        }

        public async Task TestPost()
        {
            var response = await Client
                .Post("/api/values")
                .SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

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

            Assert.Equal(JsonConvert.SerializeObject(new { Test = "body" }), await Handler.PopRequestString());
        }
    }
}
