using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Generation
{
    public sealed class PairsOfOpenRangesAndPointsOfAllPossibleRelations<
        TRange,
        TRanges,
        TPairOfRangeAndPoint,
        TPairsOfRangesAndPoints>
    : IEnumerable<TPairOfRangeAndPoint>
    where TRanges : IOpenRanges<TRange>
    where TPairsOfRangesAndPoints : IPairs<TRange, int, TPairOfRangeAndPoint>
    {
        public IEnumerator<TPairOfRangeAndPoint> GetEnumerator()
        {
            var a = default(TPairsOfRangesAndPoints);
            return Enumerable.Range(1, 7)
                .SelectMany(x => Ranges(), (x, y) => a.Pair(y, x))
                .GetEnumerator();
        }

        private static IEnumerable<TRange> Ranges()
        {
            yield return default(TRanges).Range(3, 5, false, false);
            yield return default(TRanges).Range(3, 5, false, true);
            yield return default(TRanges).Range(3, 5, true, false);
            yield return default(TRanges).Range(3, 5, true, true);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
