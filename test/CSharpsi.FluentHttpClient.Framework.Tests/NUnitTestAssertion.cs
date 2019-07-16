using CSharpsi.FluentHttpClient.TestFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.Framework.Tests
{
    public class NUnitTestAssertion : IAssertion
    {
        public void Equal<T>(T expected, T actual) => Assert.AreEqual(expected, actual);

        public void NotNull(object obj) => Assert.NotNull(obj);

        public T Single<T>(IEnumerable<T> collection)
        {
            Assert.That(collection, Has.Exactly(1).Items);
            return collection.Single();
        }

        public Task<T> ThrowsAsync<T>(Func<Task> testCode) where T : Exception => Task.FromResult(Assert.ThrowsAsync<T>(async () => await testCode()));
    }
}
