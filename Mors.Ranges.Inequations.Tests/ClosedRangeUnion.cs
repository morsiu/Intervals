using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Inequations.Tests
{
    public readonly struct ClosedRangeUnion
    {
        private readonly IEnumerable<ClosedRange> _ranges;

        private ClosedRangeUnion(IEnumerable<ClosedRange> ranges) => _ranges = ranges;

        public static implicit operator ClosedRangeUnion(ClosedRange range) => FromRange(range);

        public static ClosedRangeUnion FromRange(ClosedRange range) =>
            new ClosedRangeUnion(
                range.IsEmpty
                    ? Enumerable.Empty<ClosedRange>()
                    : Enumerable.Repeat(range, 1));

        public static ClosedRangeUnion FromInequation(Inequation<Point> inequation) =>
            new ClosedRangeUnion(inequation.ToClosedRanges<ClosedRange, Points, ClosedRanges>());

        public override bool Equals(object obj) =>
            obj is ClosedRangeUnion other && _ranges.SequenceEqual(other._ranges);

        public override int GetHashCode() =>
            _ranges.Aggregate(new HashCode(), (a, b) => { a.Add(b); return a; }, b => b.ToHashCode());

        public Inequation ToInequation() =>
            _ranges
                .Select(x => x.ToInequation())
                .DefaultIfEmpty(Inequation.False())
                .Aggregate((a, b) => Inequation.Or(a, b));

        public override string ToString() =>
            string.Join(" ∪ ", _ranges.Select(x => x.ToString()).DefaultIfEmpty("∅"));
    }
}