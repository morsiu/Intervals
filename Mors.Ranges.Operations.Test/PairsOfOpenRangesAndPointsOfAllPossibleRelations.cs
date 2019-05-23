using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    public sealed class PairsOfOpenRangesAndPointsOfAllPossibleRelations : IEnumerable<(OpenRange, int)>
    {
        public IEnumerator<(OpenRange, int)> GetEnumerator()
        {
            return new PairsOfOpenRangesAndPointsOfAllPossibleRelations<
                    OpenRange,
                    OpenRanges,
                    (OpenRange, int),
                    Pairs>()
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}