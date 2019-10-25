using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class AllEmptyOpenRanges<TPoint, TOpenRange, TOpenRanges>
        : IEnumerable<TOpenRange>
        where TOpenRanges : struct, IOpenRanges<TPoint, TOpenRange>
    {
        private readonly TPoint _point;

        public AllEmptyOpenRanges(in TPoint point) => _point = point;

        public IEnumerator<TOpenRange> GetEnumerator()
        {
            var ranges = default(TOpenRanges);
            yield return ranges.Empty();
            yield return ranges.Range(_point, _point, isStartOpen: true, isEndOpen: true);
            yield return ranges.Range(_point, _point, isStartOpen: true, isEndOpen: false);
            yield return ranges.Range(_point, _point, isStartOpen: false, isEndOpen: true);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}