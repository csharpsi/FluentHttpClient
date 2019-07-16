using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CSharpsi.FluentHttpClient.TestFramework
{
    internal class TestHttpContext
    {
        internal HttpClient Client { get; }
        internal MockedHandler Handler { get; }

        public TestHttpContext()
        {
            var baseAddress = new Uri("https://testapi");

            Handler = new MockedHandler(baseAddress);

            Client = new HttpClient(Handler)
            {
                BaseAddress = baseAddress
            };
        }
    }
}
