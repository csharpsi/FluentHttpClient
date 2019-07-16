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
    public class HttpPutTests
    {
        private HttpPutTestCoordinator coordinator;

        [SetUp]
        public void Setup()
        {
            coordinator = new HttpPutTestCoordinator(new NUnitTestAssertion());
        }

        [Test]
        public async Task TestPut() => await coordinator.TestPut();

        [Test]
        public async Task TestPut_WithBody() => await coordinator.TestPut_WithBody();
    }
}
