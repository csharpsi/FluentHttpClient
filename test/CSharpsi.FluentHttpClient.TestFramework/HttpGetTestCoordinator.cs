using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.TestFramework
{
    public class HttpGetTestCoordinator : AbstractTestBase
    {
        public HttpGetTestCoordinator(IAssertion assertion) : base(assertion)
        {
            Handler.AddJsonResponse(HttpMethod.Get, "/api/values", HttpStatusCode.OK, new { values = new[] { "1", "2", "3" } });
        }

        public async Task TestGet()
        {
            var response = await Client.Get("/api/values").SendAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();

            var expectedBody = JsonConvert.SerializeObject(new { values = new[] { "1", "2", "3" } });

            Assert.Equal(expectedBody, body);
        }
    }
}
