using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Generation.Tests
{
    internal sealed class PairsOfOpenRangesOfAllPossibleRelations : IEnumerable<PairOfOpenRanges>
    {
        public IEnumerator<PairOfOpenRanges> GetEnumerator()
        {
            return new PairsOfClosedRangesOfAllPossibleRelations()
                .SelectMany(x => x.ToOpenRangePairs())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}