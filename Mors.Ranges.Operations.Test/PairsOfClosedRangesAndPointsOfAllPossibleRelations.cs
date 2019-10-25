using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    public sealed class PairsOfClosedRangesAndPointsOfAllPossibleRelations : IEnumerable<(ClosedRange, int)>
    {
        public IEnumerator<(ClosedRange, int)> GetEnumerator()
        {
            return new PairsOfClosedRangesAndPointsOfAllPossibleRelations<
                    int,
                    ClosedRange,
                    ClosedRanges,
                    (ClosedRange, int),
                    Pairs>(Enumerable.Range(1, 7).ToArray())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}