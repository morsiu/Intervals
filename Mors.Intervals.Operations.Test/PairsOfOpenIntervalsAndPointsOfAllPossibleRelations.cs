using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mors.Intervals.Generation;

namespace Mors.Intervals.Operations.Test
{
    public sealed class PairsOfOpenIntervalsAndPointsOfAllPossibleRelations : IEnumerable<(OpenInterval, int)>
    {
        public IEnumerator<(OpenInterval, int)> GetEnumerator()
        {
            return new PairsOfOpenIntervalsAndPointsOfAllPossibleRelations<
                    int,
                    OpenInterval,
                    OpenIntervals,
                    (OpenInterval, int),
                    Pairs>(Enumerable.Range(1, 7).ToArray())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}