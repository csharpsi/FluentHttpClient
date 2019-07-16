using CSharpsi.FluentHttpClient.TestFramework;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.Framework.Tests
{
    [TestFixture]
    public class HttpClientTests
    {
        private HttpClientTestCoordinator coordinator;

        [SetUp]
        public void Setup()
        {
            coordinator = new HttpClientTestCoordinator(new NUnitTestAssertion());
        }

        [Test]
        public async Task TestAddHeader() => await coordinator.TestAddHeader();

        [Test]
        public async Task TestAddBasicAuth() => await coordinator.TestAddBasicAuth();

        [Test]
        public async Task TestAddBearerToken() => await coordinator.TestAddBearerToken();

        [Test]
        public async Task TestAddJson_WithSettings() => await coordinator.TestAddJson_WithSettings();

        [Test]
        public async Task ExceptionWhenUsingSendAsyncOnNonExtensionRequest() => await coordinator.ExceptionWhenUsingSendAsyncOnNonExtensionRequest();
    }
}
