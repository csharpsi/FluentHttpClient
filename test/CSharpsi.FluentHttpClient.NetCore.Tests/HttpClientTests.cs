using CSharpsi.FluentHttpClient.TestFramework;
using System.Threading.Tasks;
using Xunit;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    public class HttpClientTests
    {
        private readonly HttpClientTestCoordinator coordinator;

        public HttpClientTests()
        {
            coordinator = new HttpClientTestCoordinator(new XUnitTestAssertion());
        }

        [Fact]
        public async Task TestAddHeader() => await coordinator.TestAddHeader();

        [Fact]
        public async Task TestAddBasicAuth() => await coordinator.TestAddBasicAuth();

        [Fact]
        public async Task TestAddBearerToken() => await coordinator.TestAddBearerToken();

        [Fact]
        public async Task TestAddJson_WithSettings() => await coordinator.TestAddJson_WithSettings();

        [Fact]
        public async Task ExceptionWhenUsingSendAsyncOnNonExtensionRequest() => await coordinator.ExceptionWhenUsingSendAsyncOnNonExtensionRequest();
    }
}
