using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation.Tests
{
    internal sealed class PairsOfClosedRangesOfAllPossibleRelations : IEnumerable<PairOfClosedRanges>
    {
        public IEnumerator<PairOfClosedRanges> GetEnumerator()
        {
            var x = new ClosedRanges();
            var y = new Pairs();
            yield return y.Pair(x.Empty(), x.Empty()); // both empty
            yield return y.Pair(x.Range(1, 5), x.Empty()); // second empty
            yield return y.Pair(x.Empty(), x.Range(1, 5)); // first empty
            yield return y.Pair(x.Range(1, 5), x.Range(7, 11)); // first before second
            yield return y.Pair(x.Range(2, 7), x.Range(7, 11)); // first before second, first ends where second starts
            yield return y.Pair(x.Range(5, 9), x.Range(7, 11)); // first before second, intersecting
            yield return y.Pair(x.Range(7, 11), x.Range(7, 11)); // first equal to second
            yield return y.Pair(x.Range(9, 13), x.Range(7, 11)); // first after second, intersecting
            yield return y.Pair(x.Range(11, 15), x.Range(7, 11)); // first after second, first starts where second ends
            yield return y.Pair(x.Range(13, 18), x.Range(7, 11)); // first after second
            yield return y.Pair(x.Range(1, 8), x.Range(1, 4)); // second inside first, first starts where second starts
            yield return y.Pair(x.Range(1, 8), x.Range(3, 6)); // second inside first
            yield return y.Pair(x.Range(1, 8), x.Range(5, 8)); // second inside first, first ends where second ends
            yield return y.Pair(x.Range(1, 4), x.Range(1, 8)); // first inside second, first starts where second starts
            yield return y.Pair(x.Range(3, 6), x.Range(1, 8)); // first inside second
            yield return y.Pair(x.Range(5, 8), x.Range(1, 8)); // first inside second, first ends where second ends
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}