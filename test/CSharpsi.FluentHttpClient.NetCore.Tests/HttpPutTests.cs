using CSharpsi.FluentHttpClient.TestFramework;
using System.Threading.Tasks;
using Xunit;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    public class HttpPutTests
    {
        private readonly HttpPutTestCoordinator coordinator;
        public HttpPutTests()
        {
            coordinator = new HttpPutTestCoordinator(new XUnitTestAssertion());
        }

        [Fact]
        public async Task TestPut() => await coordinator.TestPut();

        [Fact]
        public async Task TestPut_WithBody() => await coordinator.TestPut_WithBody();
    }
}
