using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    public sealed class PairsOfOpenRangesOfAllPossibleRelations : IEnumerable<(OpenRange, OpenRange)>
    {
        public IEnumerator<(OpenRange, OpenRange)> GetEnumerator()
        {
            return new PairsOfOpenRangesOfAllPossibleRelations<
                    OpenRange,
                    (OpenRange, OpenRange),
                    OpenRanges,
                    Pairs>()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
