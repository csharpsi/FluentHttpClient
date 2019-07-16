using CSharpsi.FluentHttpClient.TestFramework;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.Framework.Tests
{
    [TestFixture]
    public class HttpGetTests
    {
        private HttpGetTestCoordinator coordinator;

        [SetUp]
        public void Setup()
        {
            coordinator = new HttpGetTestCoordinator(new NUnitTestAssertion());
        }

        [Test]
        public async Task TestGet() => await coordinator.TestGet();
    }
}
