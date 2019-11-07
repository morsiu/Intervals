using System.Collections;
using System.Collections.Generic;
using Mors.Intervals.Generation;

namespace Mors.Intervals.Operations.Test
{
    public sealed class PairsOfOpenIntervalsOfAllPossibleRelations : IEnumerable<(OpenInterval, OpenInterval)>
    {
        public IEnumerator<(OpenInterval, OpenInterval)> GetEnumerator()
        {
            return new PairsOfOpenIntervalsOfAllPossibleRelations<
                    OpenInterval,
                    (OpenInterval, OpenInterval),
                    OpenIntervals,
                    Pairs>()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
