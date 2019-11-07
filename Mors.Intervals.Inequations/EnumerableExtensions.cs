using System.Collections.Generic;

namespace Mors.Intervals.Inequations
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> FirstUnionLast<T>(this IEnumerable<T> sequence)
        {
            using var enumerator = sequence.GetEnumerator();
            if (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            if (!enumerator.MoveNext())
            {
                yield break;
            }
            var last = enumerator.Current;
            while (enumerator.MoveNext())
            {
                last = enumerator.Current;
            }
            yield return last;
        }
    }
}
