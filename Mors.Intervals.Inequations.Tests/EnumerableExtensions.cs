using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Inequations.Tests
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<object[]> ToEnumerableOfObjects<T>(this IEnumerable<T> a) =>
            a.Where(x => x != null).Select(x => new object[] { x! });
    }
}