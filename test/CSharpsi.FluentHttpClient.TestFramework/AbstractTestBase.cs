using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CSharpsi.FluentHttpClient.TestFramework
{
    public abstract class AbstractTestBase
    {
        private readonly TestHttpContext context;

        protected IAssertion Assert { get; }

        protected HttpClient Client => context.Client;
        protected MockedHandler Handler => context.Handler;

        public AbstractTestBase(IAssertion assertion)
        {
            context = new TestHttpContext();
            Assert = assertion;
        }
    }
}
