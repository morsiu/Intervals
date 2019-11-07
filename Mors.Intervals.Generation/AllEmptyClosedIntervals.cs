using System.Collections;
using System.Collections.Generic;

namespace Mors.Intervals.Generation
{
    public sealed class AllEmptyClosedIntervals<TPoint, TClosedInterval, TClosedIntervals>
        : IEnumerable<TClosedInterval>
        where TClosedIntervals : struct, IClosedIntervals<TPoint, TClosedInterval>
    {
        public IEnumerator<TClosedInterval> GetEnumerator()
        {
            var intervals = default(TClosedIntervals);
            yield return intervals.Empty();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}