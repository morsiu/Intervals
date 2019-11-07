using System.Collections;
using System.Collections.Generic;

namespace Mors.Intervals.Generation
{
    public sealed class PairsOfClosedIntervalsOfAllPossibleRelations<
            TPoint,
            TInterval,
            TPairOfIntervals,
            TIntervals,
            TPairsOfIntervals>
        : IEnumerable<TPairOfIntervals>
        where TIntervals : struct, IClosedIntervals<TPoint, TInterval>
        where TPairsOfIntervals : struct, IPairs<TInterval, TInterval, TPairOfIntervals>
    {
        private readonly IReadOnlyList<TPoint> _points;

        /// <param name="points">List of four points, where each is greater than and not adjacent to its predecessors.</param>
        public PairsOfClosedIntervalsOfAllPossibleRelations(
            IReadOnlyList<TPoint> points) =>
            _points = points;

        public IEnumerator<TPairOfIntervals> GetEnumerator()
        {
            var x = default(TIntervals);
            var y = default(TPairsOfIntervals);
            var a = _points[0];
            var b = _points[1];
            var c = _points[2];
            var d = _points[3];
            yield return y.Pair(x.Empty(), x.Empty());
            yield return y.Pair(x.Interval(a, b), x.Empty());
            yield return y.Pair(x.Empty(), x.Interval(a, b));
            yield return y.Pair(x.Interval(a, b), x.Interval(c, d));
            yield return y.Pair(x.Interval(a, b), x.Interval(b, c));
            yield return y.Pair(x.Interval(a, c), x.Interval(b, d));
            yield return y.Pair(x.Interval(a, b), x.Interval(a, b));
            yield return y.Pair(x.Interval(b, d), x.Interval(a, c));
            yield return y.Pair(x.Interval(b, c), x.Interval(a, b));
            yield return y.Pair(x.Interval(c, d), x.Interval(a, b));
            yield return y.Pair(x.Interval(a, c), x.Interval(a, b));
            yield return y.Pair(x.Interval(a, d), x.Interval(b, c));
            yield return y.Pair(x.Interval(a, c), x.Interval(b, c));
            yield return y.Pair(x.Interval(a, b), x.Interval(a, c));
            yield return y.Pair(x.Interval(b, c), x.Interval(a, d));
            yield return y.Pair(x.Interval(b, c), x.Interval(a, c));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}