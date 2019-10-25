using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class AllNonEmptyOpenRanges<TPoint, TOpenRange, TOpenRanges>
        : IEnumerable<TOpenRange>
        where TOpenRanges : struct, IOpenRanges<TPoint, TOpenRange>
    {
        private readonly TPoint _first;
        private readonly TPoint _second;

        /// <param name="first">The first value, must be smaller than the second.</param>
        /// <param name="second">The second value, must be greater than the first.</param>
        public AllNonEmptyOpenRanges(in TPoint first, in TPoint second)
        {
            _first = first;
            _second = second;
        }

        public IEnumerator<TOpenRange> GetEnumerator()
        {
            var ranges = default(TOpenRanges);
            yield return ranges.Range(_first, _second, isStartOpen: false, isEndOpen: false);
            yield return ranges.Range(_first, _second, isStartOpen: false, isEndOpen: true);
            yield return ranges.Range(_first, _second, isStartOpen: true, isEndOpen: false);
            yield return ranges.Range(_first, _second, isStartOpen: true, isEndOpen: true);
            yield return ranges.Range(_first, _first, isStartOpen: false, isEndOpen: false);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}