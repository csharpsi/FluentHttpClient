using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharpsi.FluentHttpClient.TestFramework
{
    public interface IAssertion
    {
        void NotNull(object obj);
        void Equal<T>(T expected, T actual);
        T Single<T>(IEnumerable<T> collection);
        Task<T> ThrowsAsync<T>(Func<Task> testCode) where T : Exception;
    }
}
