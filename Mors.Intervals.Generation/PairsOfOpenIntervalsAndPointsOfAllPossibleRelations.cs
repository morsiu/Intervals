using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Generation
{
    public sealed class PairsOfOpenIntervalsAndPointsOfAllPossibleRelations<
        TPoint,
        TInterval,
        TIntervals,
        TPairOfIntervalAndPoint,
        TPairsOfIntervalsAndPoints>
    : IEnumerable<TPairOfIntervalAndPoint>
    where TIntervals : struct, IOpenIntervals<TPoint, TInterval>
    where TPairsOfIntervalsAndPoints : struct, IPairs<TInterval, TPoint, TPairOfIntervalAndPoint>
    {
        private readonly IReadOnlyList<TPoint> _points;

        /// <param name="points">List of seven points, where each is greater than its predecessors.</param>
        public PairsOfOpenIntervalsAndPointsOfAllPossibleRelations(
            IReadOnlyList<TPoint> points) =>
            _points = points;

        public IEnumerator<TPairOfIntervalAndPoint> GetEnumerator()
        {
            var a = default(TPairsOfIntervalsAndPoints);
            return _points
                .SelectMany(
                    x => Intervals(_points[2], _points[4]),
                    (x, y) => a.Pair(y, x))
                .GetEnumerator();
        }

        private static IEnumerable<TInterval> Intervals(TPoint start, TPoint end)
        {
            yield return default(TIntervals).Interval(start, end, false, false);
            yield return default(TIntervals).Interval(start, end, false, true);
            yield return default(TIntervals).Interval(start, end, true, false);
            yield return default(TIntervals).Interval(start, end, true, true);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
