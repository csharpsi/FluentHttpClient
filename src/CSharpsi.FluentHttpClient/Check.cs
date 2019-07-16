using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpsi.FluentHttpClient
{
    internal static class Check
    {
        internal static void NotNull(object obj, string name)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }        

        internal static void AtLeastOne<T>(IEnumerable<T> input, string name)
        {
            NotNull(input, name);

            if (!input.Any())
            {
                throw new ArgumentException("At least one value is required", name);
            }
        }
    }
}
