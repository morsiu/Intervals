using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Generation.Tests
{
    internal sealed class PairsOfOpenIntervalsOfAllPossibleRelations : IEnumerable<PairOfOpenIntervals>
    {
        public IEnumerator<PairOfOpenIntervals> GetEnumerator()
        {
            return new PairsOfClosedIntervalsOfAllPossibleRelations()
                .SelectMany(x => x.ToOpenIntervalPairs())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}