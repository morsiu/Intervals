using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Generation
{
    public sealed class PairsOfClosedRangesAndPointsOfAllPossibleRelations<
            TRange,
            TRanges,
            TPairOfRangeAndPoint,
            TPairsOfRangesAndPoints>
        : IEnumerable<TPairOfRangeAndPoint>
        where TRanges : struct, IClosedRanges<TRange>
        where TPairsOfRangesAndPoints : struct, IPairs<TRange, int, TPairOfRangeAndPoint>
    {
        public IEnumerator<TPairOfRangeAndPoint> GetEnumerator()
        {
            var a = default(TRanges).Range(3, 5);
            var b = default(TPairsOfRangesAndPoints);
            return Enumerable.Range(1, 7)
                .Select(x => b.Pair(a, x))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
