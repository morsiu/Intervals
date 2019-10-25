using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class AllEmptyOpenRanges<TOpenRange, TOpenRanges> : IEnumerable<TOpenRange>
        where TOpenRanges : struct, IOpenRanges<int, TOpenRange>
    {
        public IEnumerator<TOpenRange> GetEnumerator()
        {
            var ranges = default(TOpenRanges);
            yield return ranges.Empty();
            yield return ranges.Range(1, 1, isStartOpen: true, isEndOpen: true);
            yield return ranges.Range(1, 1, isStartOpen: true, isEndOpen: false);
            yield return ranges.Range(1, 1, isStartOpen: false, isEndOpen: true);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}