using System.Collections;
using System.Collections.Generic;
using Mors.Intervals.Generation;

namespace Mors.Intervals.Operations.Test
{
    public sealed class PairsOfClosedIntervalsOfAllPossibleRelations : IEnumerable<(ClosedInterval, ClosedInterval)>
    {
        public IEnumerator<(ClosedInterval, ClosedInterval)> GetEnumerator()
        {
            return new PairsOfClosedIntervalsOfAllPossibleRelations<
                    int,
                    ClosedInterval,
                    (ClosedInterval, ClosedInterval),
                    ClosedIntervals,
                    Pairs>(new[] { 1, 3, 5, 7 })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
