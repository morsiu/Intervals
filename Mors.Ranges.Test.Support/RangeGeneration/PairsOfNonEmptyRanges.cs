using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Test.Support.RangeGeneration.Options;

namespace Mors.Ranges.Test.Support.RangeGeneration
{
    internal sealed class PairsOfNonEmptyRanges : IEnumerable<RangePair>
    {
        public IEnumerator<RangePair> GetEnumerator()
        {
            foreach (var abRangesRelation in new AllRangeRelations())
            {
                foreach (var rangeAEnds in new AllRangeEnds())
                {
                    foreach (var rangeBEnds in new AllRangeEnds())
                    {
                        yield return new PairOfRangesInARelation(abRangesRelation).RangePair(rangeAEnds, rangeBEnds);
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
