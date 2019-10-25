using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    public sealed class PairsOfOpenRangesAndPointsOfAllPossibleRelations : IEnumerable<(OpenRange, int)>
    {
        public IEnumerator<(OpenRange, int)> GetEnumerator()
        {
            return new PairsOfOpenRangesAndPointsOfAllPossibleRelations<
                    int,
                    OpenRange,
                    OpenRanges,
                    (OpenRange, int),
                    Pairs>(Enumerable.Range(1, 7).ToArray())
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}