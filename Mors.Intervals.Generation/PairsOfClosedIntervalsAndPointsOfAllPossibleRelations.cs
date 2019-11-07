using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Generation
{
    public sealed class PairsOfClosedIntervalsAndPointsOfAllPossibleRelations<
            TPoint,
            TInterval,
            TIntervals,
            TPairOfIntervalAndPoint,
            TPairsOfIntervalsAndPoints>
        : IEnumerable<TPairOfIntervalAndPoint>
        where TIntervals : struct, IClosedIntervals<TPoint, TInterval>
        where TPairsOfIntervalsAndPoints : struct, IPairs<TInterval, TPoint, TPairOfIntervalAndPoint>
    {
        private readonly IReadOnlyList<TPoint> _points;

        /// <param name="points">List of seven points, where each is greater than its predecessors.</param>
        public PairsOfClosedIntervalsAndPointsOfAllPossibleRelations(
            IReadOnlyList<TPoint> points) =>
            _points = points;

        public IEnumerator<TPairOfIntervalAndPoint> GetEnumerator()
        {
            var a = default(TIntervals).Interval(_points[2], _points[4]);
            var b = default(TPairsOfIntervalsAndPoints);
            return _points
                .Select(x => b.Pair(a, x))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
