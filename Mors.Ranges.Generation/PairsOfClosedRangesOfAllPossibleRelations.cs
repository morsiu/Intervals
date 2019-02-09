using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class PairsOfClosedRangesOfAllPossibleRelations<TRange, TRangePair, TRanges, TRangePairs> : IEnumerable<TRangePair>
        where TRanges : IClosedRanges<TRange>
        where TRangePairs : IPairs<TRange, TRange, TRangePair>
    {
        public IEnumerator<TRangePair> GetEnumerator()
        {
            var x = default(TRanges);
            var y = default(TRangePairs);
            yield return y.Pair(x.Empty(), x.Empty());
            yield return y.Pair(x.Range(1, 5), x.Empty());
            yield return y.Pair(x.Empty(), x.Range(1, 5));
            yield return y.Pair(x.Range(1, 5), x.Range(7, 11));
            yield return y.Pair(x.Range(2, 7), x.Range(7, 11));
            yield return y.Pair(x.Range(5, 9), x.Range(7, 11));
            yield return y.Pair(x.Range(7, 11), x.Range(7, 11));
            yield return y.Pair(x.Range(9, 13), x.Range(7, 11));
            yield return y.Pair(x.Range(11, 15), x.Range(7, 11));
            yield return y.Pair(x.Range(13, 18), x.Range(7, 11));
            yield return y.Pair(x.Range(1, 8), x.Range(1, 4));
            yield return y.Pair(x.Range(1, 8), x.Range(3, 6));
            yield return y.Pair(x.Range(1, 8), x.Range(5, 8));
            yield return y.Pair(x.Range(1, 4), x.Range(1, 8));
            yield return y.Pair(x.Range(3, 6), x.Range(1, 8));
            yield return y.Pair(x.Range(5, 8), x.Range(1, 8));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
