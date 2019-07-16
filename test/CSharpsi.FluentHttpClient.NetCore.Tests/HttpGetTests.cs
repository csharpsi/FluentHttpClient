using CSharpsi.FluentHttpClient.TestFramework;
using System.Threading.Tasks;
using Xunit;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    public class HttpGetTests
    {
        private readonly HttpGetTestCoordinator coordinator;

        public HttpGetTests()
        {
            coordinator = new HttpGetTestCoordinator(new XUnitTestAssertion());
        }

        [Fact]
        public async Task TestGet() => await coordinator.TestGet();
    }
}
