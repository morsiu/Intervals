using System.Collections;
using System.Collections.Generic;

namespace Mors.Intervals.Generation
{
    public sealed class AllNonEmptyOpenIntervals<TPoint, TOpenInterval, TOpenIntervals>
        : IEnumerable<TOpenInterval>
        where TOpenIntervals : struct, IOpenIntervals<TPoint, TOpenInterval>
    {
        private readonly TPoint _first;
        private readonly TPoint _second;

        /// <param name="first">The first value, must be smaller than the second.</param>
        /// <param name="second">The second value, must be greater than the first.</param>
        public AllNonEmptyOpenIntervals(in TPoint first, in TPoint second)
        {
            _first = first;
            _second = second;
        }

        public IEnumerator<TOpenInterval> GetEnumerator()
        {
            var intervals = default(TOpenIntervals);
            yield return intervals.Interval(_first, _second, isStartOpen: false, isEndOpen: false);
            yield return intervals.Interval(_first, _second, isStartOpen: false, isEndOpen: true);
            yield return intervals.Interval(_first, _second, isStartOpen: true, isEndOpen: false);
            yield return intervals.Interval(_first, _second, isStartOpen: true, isEndOpen: true);
            yield return intervals.Interval(_first, _first, isStartOpen: false, isEndOpen: false);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}