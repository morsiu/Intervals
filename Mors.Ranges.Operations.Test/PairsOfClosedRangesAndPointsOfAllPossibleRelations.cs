using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    public sealed class PairsOfClosedRangesAndPointsOfAllPossibleRelations : IEnumerable<(ClosedRange, int)>
    {
        public IEnumerator<(ClosedRange, int)> GetEnumerator()
        {
            return new PairsOfClosedRangesAndPointsOfAllPossibleRelations<
                    ClosedRange,
                    ClosedRanges,
                    (ClosedRange, int),
                    Pairs>()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}