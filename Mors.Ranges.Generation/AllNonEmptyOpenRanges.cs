using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class AllNonEmptyOpenRanges<TOpenRange, TOpenRanges> : IEnumerable<TOpenRange>
        where TOpenRanges : struct, IOpenRanges<int, TOpenRange>
    {
        public IEnumerator<TOpenRange> GetEnumerator()
        {
            var ranges = default(TOpenRanges);
            yield return ranges.Range(1, 5, isStartOpen: false, isEndOpen: false);
            yield return ranges.Range(1, 5, isStartOpen: false, isEndOpen: true);
            yield return ranges.Range(1, 5, isStartOpen: true, isEndOpen: false);
            yield return ranges.Range(1, 5, isStartOpen: true, isEndOpen: true);
            yield return ranges.Range(1, 1, isStartOpen: false, isEndOpen: false);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}