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
            var a = 1;
            var b = 3;
            var c = 5;
            var d = 7;
            yield return y.Pair(x.Empty(), x.Empty()); // both empty
            yield return y.Pair(x.Range(a, b), x.Empty()); // second empty
            yield return y.Pair(x.Empty(), x.Range(a, b)); // first empty
            yield return y.Pair(x.Range(a, b), x.Range(c, d)); // first before second
            yield return y.Pair(x.Range(a, b), x.Range(b, c)); // first before second, first ends where second starts
            yield return y.Pair(x.Range(a, c), x.Range(b, d)); // first before second, intersecting
            yield return y.Pair(x.Range(a, b), x.Range(a, b)); // first equal to second
            yield return y.Pair(x.Range(b, d), x.Range(a, c)); // first after second, intersecting
            yield return y.Pair(x.Range(b, c), x.Range(a, b)); // first after second, first starts where second ends
            yield return y.Pair(x.Range(c, d), x.Range(a, b)); // first after second
            yield return y.Pair(x.Range(a, c), x.Range(a, b)); // second inside first, first starts where second starts
            yield return y.Pair(x.Range(a, d), x.Range(b, c)); // second inside first
            yield return y.Pair(x.Range(a, c), x.Range(b, c)); // second inside first, first ends where second ends
            yield return y.Pair(x.Range(a, b), x.Range(a, c)); // first inside second, first starts where second starts
            yield return y.Pair(x.Range(b, c), x.Range(a, d)); // first inside second
            yield return y.Pair(x.Range(b, c), x.Range(a, c)); // first inside second, first ends where second ends
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}