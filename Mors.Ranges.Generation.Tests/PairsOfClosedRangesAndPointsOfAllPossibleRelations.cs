using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation.Tests
{
    internal sealed class PairsOfClosedRangesAndPointsOfAllPossibleRelations
        : IEnumerable<PairOfClosedRangeAndPoint>
    {
        public IEnumerator<PairOfClosedRangeAndPoint> GetEnumerator()
        {
            var x = new ClosedRange(3, 5);
            yield return new PairOfClosedRangeAndPoint(x, 1); // range starts after the point
            yield return new PairOfClosedRangeAndPoint(x, 2); // range starts after the point, with adjacent start
            yield return new PairOfClosedRangeAndPoint(x, 3); // range starts with the point
            yield return new PairOfClosedRangeAndPoint(x, 4); // range contains the point
            yield return new PairOfClosedRangeAndPoint(x, 5); // range ends with the point
            yield return new PairOfClosedRangeAndPoint(x, 6); // range ends before the point, with adjacent end
            yield return new PairOfClosedRangeAndPoint(x, 7); // range ends before the point
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
