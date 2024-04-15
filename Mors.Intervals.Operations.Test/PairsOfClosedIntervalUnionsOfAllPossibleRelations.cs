using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mors.Intervals.Generation;

namespace Mors.Intervals.Operations.Test
{
    internal sealed class PairsOfClosedIntervalUnionsOfAllPossibleRelations
        : IEnumerable<(ClosedIntervalUnion, ClosedIntervalUnion)>
    {
        public IEnumerator<(ClosedIntervalUnion, ClosedIntervalUnion)> GetEnumerator()
        {
            return new PairsOfClosedIntervalsOfAllPossibleRelations<
                    int,
                    ClosedInterval,
                    (ClosedInterval, ClosedInterval),
                    ClosedIntervals,
                    Pairs>([1, 3, 5, 7])
                .Select(x => (ClosedIntervalUnion(x.Item1), ClosedIntervalUnion(x.Item2)))
                .GetEnumerator();

            static ClosedIntervalUnion ClosedIntervalUnion(ClosedInterval interval) =>
                interval.Empty
                    ? new ClosedIntervalUnion([])
                    : new ClosedIntervalUnion([interval]);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
