using System.Collections;
using System.Collections.Generic;

namespace Mors.Intervals.Generation
{
    public sealed class AllNonEmptyClosedIntervals<TPoint, TClosedInterval, TClosedIntervals>
        : IEnumerable<TClosedInterval>
        where TClosedIntervals : struct, IClosedIntervals<TPoint, TClosedInterval>
    {
        private readonly TPoint _first;
        private readonly TPoint _second;

        /// <param name="first">The first value, must be smaller than the second.</param>
        /// <param name="second">The second value, must be greater than the first.</param>
        public AllNonEmptyClosedIntervals(in TPoint first, in TPoint second)
        {
            _first = first;
            _second = second;
        }

        public IEnumerator<TClosedInterval> GetEnumerator()
        {
            var intervals = default(TClosedIntervals);
            yield return intervals.Interval(_first, _first);
            yield return intervals.Interval(_first, _second);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}