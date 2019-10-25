using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Generation
{
    public sealed class PairsOfClosedRangesAndPointsOfAllPossibleRelations<
            TPoint,
            TRange,
            TRanges,
            TPairOfRangeAndPoint,
            TPairsOfRangesAndPoints>
        : IEnumerable<TPairOfRangeAndPoint>
        where TRanges : struct, IClosedRanges<TPoint, TRange>
        where TPairsOfRangesAndPoints : struct, IPairs<TRange, TPoint, TPairOfRangeAndPoint>
    {
        private readonly IReadOnlyList<TPoint> _points;

        /// <param name="points">List of seven points, where each is greater than its predecessors.</param>
        public PairsOfClosedRangesAndPointsOfAllPossibleRelations(
            IReadOnlyList<TPoint> points) =>
            _points = points;

        public IEnumerator<TPairOfRangeAndPoint> GetEnumerator()
        {
            var a = default(TRanges).Range(_points[2], _points[4]);
            var b = default(TPairsOfRangesAndPoints);
            return _points
                .Select(x => b.Pair(a, x))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
