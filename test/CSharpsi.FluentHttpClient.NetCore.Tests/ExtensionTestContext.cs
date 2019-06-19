using System;
using System.Net.Http;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    internal class ExtensionTestContext
    {
        internal HttpClient Client { get; }
        internal MockHttpHandler Handler { get; }

        public ExtensionTestContext()
        {
            var baseAddress = new Uri("https://testapi");

            Handler = new MockHttpHandler(baseAddress);

            Client = new HttpClient(Handler)
            {
                BaseAddress = baseAddress
            };            
        }
    }
}
