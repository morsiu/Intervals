using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class AllEmptyClosedRanges<TPoint, TClosedRange, TClosedRanges>
        : IEnumerable<TClosedRange>
        where TClosedRanges : struct, IClosedRanges<TPoint, TClosedRange>
    {
        public IEnumerator<TClosedRange> GetEnumerator()
        {
            var ranges = default(TClosedRanges);
            yield return ranges.Empty();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}