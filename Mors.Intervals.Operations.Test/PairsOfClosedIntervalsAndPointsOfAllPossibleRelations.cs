using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mors.Intervals.Generation;

namespace Mors.Intervals.Operations.Test
{
    public sealed class PairsOfClosedIntervalsAndPointsOfAllPossibleRelations : IEnumerable<(ClosedInterval, int)>
    {
        public IEnumerator<(ClosedInterval, int)> GetEnumerator()
        {
            return new PairsOfClosedIntervalsAndPointsOfAllPossibleRelations<
                    int,
                    ClosedInterval,
                    ClosedIntervals,
                    (ClosedInterval, int),
                    Pairs>(Enumerable.Range(1, 7).ToArray())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}