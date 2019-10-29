using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class AllNonEmptyClosedRanges<TPoint, TClosedRange, TClosedRanges>
        : IEnumerable<TClosedRange>
        where TClosedRanges : struct, IClosedRanges<TPoint, TClosedRange>
    {
        private readonly TPoint _first;
        private readonly TPoint _second;

        /// <param name="first">The first value, must be smaller than the second.</param>
        /// <param name="second">The second value, must be greater than the first.</param>
        public AllNonEmptyClosedRanges(in TPoint first, in TPoint second)
        {
            _first = first;
            _second = second;
        }

        public IEnumerator<TClosedRange> GetEnumerator()
        {
            var ranges = default(TClosedRanges);
            yield return ranges.Range(_first, _first);
            yield return ranges.Range(_first, _second);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}