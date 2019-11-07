using System.Collections;
using System.Collections.Generic;

namespace Mors.Intervals.Generation
{
    public sealed class AllEmptyOpenIntervals<TPoint, TOpenInterval, TOpenIntervals>
        : IEnumerable<TOpenInterval>
        where TOpenIntervals : struct, IOpenIntervals<TPoint, TOpenInterval>
    {
        private readonly TPoint _point;

        public AllEmptyOpenIntervals(in TPoint point) => _point = point;

        public IEnumerator<TOpenInterval> GetEnumerator()
        {
            var intervals = default(TOpenIntervals);
            yield return intervals.Empty();
            yield return intervals.Interval(_point, _point, isStartOpen: true, isEndOpen: true);
            yield return intervals.Interval(_point, _point, isStartOpen: true, isEndOpen: false);
            yield return intervals.Interval(_point, _point, isStartOpen: false, isEndOpen: true);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}