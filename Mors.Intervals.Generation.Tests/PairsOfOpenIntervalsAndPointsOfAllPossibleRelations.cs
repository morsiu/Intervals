using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Generation.Tests
{
    internal sealed class PairsOfOpenIntervalsAndPointsOfAllPossibleRelations
        : IEnumerable<PairOfOpenIntervalAndPoint>
    {
        public IEnumerator<PairOfOpenIntervalAndPoint> GetEnumerator()
        {
            return Intervals().SelectMany(Pairs).GetEnumerator();
        }

        private static IEnumerable<OpenInterval> Intervals()
        {
            yield return new OpenInterval(3, 5, false, false);
            yield return new OpenInterval(3, 5, false, true);
            yield return new OpenInterval(3, 5, true, false);
            yield return new OpenInterval(3, 5, true, true);
        }

        private static IEnumerable<PairOfOpenIntervalAndPoint> Pairs(OpenInterval x)
        {
            yield return new PairOfOpenIntervalAndPoint(x, 1); // interval starts after the point
            yield return new PairOfOpenIntervalAndPoint(x, 2); // interval starts after the point, with adjacent start
            yield return new PairOfOpenIntervalAndPoint(x, 3); // interval starts with the point
            yield return new PairOfOpenIntervalAndPoint(x, 4); // interval contains the point
            yield return new PairOfOpenIntervalAndPoint(x, 5); // interval ends with the point
            yield return new PairOfOpenIntervalAndPoint(x, 6); // interval ends before the point, with adjacent end
            yield return new PairOfOpenIntervalAndPoint(x, 7); // interval ends before the point
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
