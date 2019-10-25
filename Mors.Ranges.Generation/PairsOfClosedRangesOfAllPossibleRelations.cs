using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Generation
{
    public sealed class PairsOfClosedRangesOfAllPossibleRelations<
            TPoint,
            TRange,
            TPairOfRanges,
            TRanges,
            TPairsOfRanges>
        : IEnumerable<TPairOfRanges>
        where TRanges : struct, IClosedRanges<TPoint, TRange>
        where TPairsOfRanges : struct, IPairs<TRange, TRange, TPairOfRanges>
    {
        private readonly IReadOnlyList<TPoint> _points;

        /// <param name="points">List of four points, where each is greater than and not adjacent to its predecessors.</param>
        public PairsOfClosedRangesOfAllPossibleRelations(
            IReadOnlyList<TPoint> points) =>
            _points = points;

        public IEnumerator<TPairOfRanges> GetEnumerator()
        {
            var x = default(TRanges);
            var y = default(TPairsOfRanges);
            var a = _points[0];
            var b = _points[1];
            var c = _points[2];
            var d = _points[3];
            yield return y.Pair(x.Empty(), x.Empty());
            yield return y.Pair(x.Range(a, b), x.Empty());
            yield return y.Pair(x.Empty(), x.Range(a, b));
            yield return y.Pair(x.Range(a, b), x.Range(c, d));
            yield return y.Pair(x.Range(a, b), x.Range(b, c));
            yield return y.Pair(x.Range(a, c), x.Range(b, d));
            yield return y.Pair(x.Range(a, b), x.Range(a, b));
            yield return y.Pair(x.Range(b, d), x.Range(a, c));
            yield return y.Pair(x.Range(b, c), x.Range(a, b));
            yield return y.Pair(x.Range(c, d), x.Range(a, b));
            yield return y.Pair(x.Range(a, c), x.Range(a, b));
            yield return y.Pair(x.Range(a, d), x.Range(b, c));
            yield return y.Pair(x.Range(a, c), x.Range(b, c));
            yield return y.Pair(x.Range(a, b), x.Range(a, c));
            yield return y.Pair(x.Range(b, c), x.Range(a, d));
            yield return y.Pair(x.Range(b, c), x.Range(a, c));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}