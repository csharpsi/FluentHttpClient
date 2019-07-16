using CSharpsi.FluentHttpClient.TestFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CSharpsi.FluentHttpClient.NetCore.Tests
{
    public class XUnitTestAssertion : IAssertion
    {
        public void Equal<T>(T expected, T actual) => Assert.Equal(expected, actual);

        public void NotNull(object obj) => Assert.NotNull(obj);

        public T Single<T>(IEnumerable<T> collection) => Assert.Single(collection);

        public async Task<T> ThrowsAsync<T>(Func<Task> testCode) where T : Exception => await Assert.ThrowsAsync<T>(testCode);
    }
}
