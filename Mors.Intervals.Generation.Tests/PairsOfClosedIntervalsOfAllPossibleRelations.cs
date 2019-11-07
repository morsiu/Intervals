using System.Collections;
using System.Collections.Generic;

namespace Mors.Intervals.Generation.Tests
{
    internal sealed class PairsOfClosedIntervalsOfAllPossibleRelations : IEnumerable<PairOfClosedIntervals>
    {
        public IEnumerator<PairOfClosedIntervals> GetEnumerator()
        {
            var x = new ClosedIntervals();
            var y = new Pairs();
            var a = 1;
            var b = 3;
            var c = 5;
            var d = 7;
            yield return y.Pair(x.Empty(), x.Empty()); // both empty
            yield return y.Pair(x.Interval(a, b), x.Empty()); // second empty
            yield return y.Pair(x.Empty(), x.Interval(a, b)); // first empty
            yield return y.Pair(x.Interval(a, b), x.Interval(c, d)); // first before second
            yield return y.Pair(x.Interval(a, b), x.Interval(b, c)); // first before second, first ends where second starts
            yield return y.Pair(x.Interval(a, c), x.Interval(b, d)); // first before second, intersecting
            yield return y.Pair(x.Interval(a, b), x.Interval(a, b)); // first equal to second
            yield return y.Pair(x.Interval(b, d), x.Interval(a, c)); // first after second, intersecting
            yield return y.Pair(x.Interval(b, c), x.Interval(a, b)); // first after second, first starts where second ends
            yield return y.Pair(x.Interval(c, d), x.Interval(a, b)); // first after second
            yield return y.Pair(x.Interval(a, c), x.Interval(a, b)); // second inside first, first starts where second starts
            yield return y.Pair(x.Interval(a, d), x.Interval(b, c)); // second inside first
            yield return y.Pair(x.Interval(a, c), x.Interval(b, c)); // second inside first, first ends where second ends
            yield return y.Pair(x.Interval(a, b), x.Interval(a, c)); // first inside second, first starts where second starts
            yield return y.Pair(x.Interval(b, c), x.Interval(a, d)); // first inside second
            yield return y.Pair(x.Interval(b, c), x.Interval(a, c)); // first inside second, first ends where second ends
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}