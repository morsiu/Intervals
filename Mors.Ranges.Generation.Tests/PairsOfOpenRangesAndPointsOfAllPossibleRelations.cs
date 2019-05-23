using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Generation.Tests
{
    internal sealed class PairsOfOpenRangesAndPointsOfAllPossibleRelations
        : IEnumerable<PairOfOpenRangeAndPoint>
    {
        public IEnumerator<PairOfOpenRangeAndPoint> GetEnumerator()
        {
            return Ranges().SelectMany(Pairs).GetEnumerator();
        }

        private static IEnumerable<OpenRange> Ranges()
        {
            yield return new OpenRange(3, 5, false, false);
            yield return new OpenRange(3, 5, false, true);
            yield return new OpenRange(3, 5, true, false);
            yield return new OpenRange(3, 5, true, true);
        }

        private static IEnumerable<PairOfOpenRangeAndPoint> Pairs(OpenRange x)
        {
            yield return new PairOfOpenRangeAndPoint(x, 1); // range starts after the point
            yield return new PairOfOpenRangeAndPoint(x, 2); // range starts after the point, with adjacent start
            yield return new PairOfOpenRangeAndPoint(x, 3); // range starts with the point
            yield return new PairOfOpenRangeAndPoint(x, 4); // range contains the point
            yield return new PairOfOpenRangeAndPoint(x, 5); // range ends with the point
            yield return new PairOfOpenRangeAndPoint(x, 6); // range ends before the point, with adjacent end
            yield return new PairOfOpenRangeAndPoint(x, 7); // range ends before the point
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
