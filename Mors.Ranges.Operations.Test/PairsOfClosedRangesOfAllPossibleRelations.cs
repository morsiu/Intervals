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
                    ClosedRange,
                    (ClosedRange, ClosedRange),
                    ClosedRanges,
                    Pairs>()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
