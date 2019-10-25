using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class AllClosedRanges<TClosedRange, TClosedRanges> : IEnumerable<TClosedRange>
        where TClosedRanges : struct, IClosedRanges<int, TClosedRange>
    {
        public IEnumerator<TClosedRange> GetEnumerator()
        {
            var ranges = default(TClosedRanges);
            yield return ranges.Empty();
            yield return ranges.Range(1, 1);
            yield return ranges.Range(1, 5);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}