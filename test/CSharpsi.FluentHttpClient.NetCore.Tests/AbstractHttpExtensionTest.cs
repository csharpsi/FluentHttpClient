using System.Net.Http;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    public abstract class AbstractHttpExtensionTest
    {
        private readonly ExtensionTestContext context;
        protected HttpClient Client => context.Client;
        protected MockHttpHandler Handler => context.Handler;

        protected AbstractHttpExtensionTest()
        {
            context = new ExtensionTestContext();
        }
    }
}
