using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    public sealed class PairsOfClosedRangesOfAllPossibleRelations : IEnumerable<(ClosedRange, ClosedRange)>
    {
        public IEnumerator<(ClosedRange, ClosedRange)> GetEnumerator()
        {
            return new PairsOfClosedRangesOfAllPossibleRelations<
                    int,
                    ClosedRange,
                    (ClosedRange, ClosedRange),
                    ClosedRanges,
                    Pairs>(new[] { 1, 3, 5, 7 })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
