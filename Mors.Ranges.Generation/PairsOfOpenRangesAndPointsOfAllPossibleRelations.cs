using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Generation
{
    public sealed class PairsOfOpenRangesAndPointsOfAllPossibleRelations<
        TPoint,
        TRange,
        TRanges,
        TPairOfRangeAndPoint,
        TPairsOfRangesAndPoints>
    : IEnumerable<TPairOfRangeAndPoint>
    where TRanges : struct, IOpenRanges<TPoint, TRange>
    where TPairsOfRangesAndPoints : struct, IPairs<TRange, TPoint, TPairOfRangeAndPoint>
    {
        private readonly IReadOnlyList<TPoint> _points;

        /// <param name="points">List of seven points, where each is greater than its predecessors.</param>
        public PairsOfOpenRangesAndPointsOfAllPossibleRelations(
            IReadOnlyList<TPoint> points) =>
            _points = points;

        public IEnumerator<TPairOfRangeAndPoint> GetEnumerator()
        {
            var a = default(TPairsOfRangesAndPoints);
            return _points
                .SelectMany(
                    x => Ranges(_points[2], _points[4]),
                    (x, y) => a.Pair(y, x))
                .GetEnumerator();
        }

        private static IEnumerable<TRange> Ranges(TPoint start, TPoint end)
        {
            yield return default(TRanges).Range(start, end, false, false);
            yield return default(TRanges).Range(start, end, false, true);
            yield return default(TRanges).Range(start, end, true, false);
            yield return default(TRanges).Range(start, end, true, true);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
