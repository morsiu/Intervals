using System.Collections;
using System.Collections.Generic;

namespace Mors.Intervals.Generation.Tests
{
    internal sealed class PairsOfClosedIntervalsAndPointsOfAllPossibleRelations
        : IEnumerable<PairOfClosedIntervalAndPoint>
    {
        public IEnumerator<PairOfClosedIntervalAndPoint> GetEnumerator()
        {
            var x = new ClosedInterval(3, 5);
            yield return new PairOfClosedIntervalAndPoint(x, 1); // interval starts after the point
            yield return new PairOfClosedIntervalAndPoint(x, 2); // interval starts after the point, with adjacent start
            yield return new PairOfClosedIntervalAndPoint(x, 3); // interval starts with the point
            yield return new PairOfClosedIntervalAndPoint(x, 4); // interval contains the point
            yield return new PairOfClosedIntervalAndPoint(x, 5); // interval ends with the point
            yield return new PairOfClosedIntervalAndPoint(x, 6); // interval ends before the point, with adjacent end
            yield return new PairOfClosedIntervalAndPoint(x, 7); // interval ends before the point
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
