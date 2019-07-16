using CSharpsi.FluentHttpClient.TestFramework;
using System.Threading.Tasks;
using Xunit;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    public class HttpPostTests
    {
        private readonly HttpPostTestCoordinator coordinator; 
        public HttpPostTests()
        {
            coordinator = new HttpPostTestCoordinator(new XUnitTestAssertion());
        }

        [Fact]
        public async Task TestPost() => await coordinator.TestPost();

        [Fact]
        public async Task TestPost_WithBody() => await coordinator.TestPost_WithBody();        
    }
}
