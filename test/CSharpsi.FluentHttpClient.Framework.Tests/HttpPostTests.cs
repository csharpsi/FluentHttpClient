using CSharpsi.FluentHttpClient.TestFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.Framework.Tests
{
    [TestFixture]
    public class HttpPostTests
    {
        private HttpPostTestCoordinator coordinator;

        [SetUp]
        public void Setup()
        {
            coordinator = new HttpPostTestCoordinator(new NUnitTestAssertion());
        }

        [Test]
        public async Task TestPost() => await coordinator.TestPost();

        [Test]
        public async Task TestPost_WithBody() => await coordinator.TestPost_WithBody();
    }
}
