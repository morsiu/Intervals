using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Inequations.Tests
{
    using Implementation = Inequations.Inequation;

    public readonly struct RangeUnion
    {
        private readonly IEnumerable<Range> _ranges;

        private RangeUnion(IEnumerable<Range> ranges) => _ranges = ranges;

        public static implicit operator RangeUnion(Range range) => FromRange(range);

        public static RangeUnion FromRange(Range range) => new RangeUnion(Enumerable.Repeat(range, 1));

        public static RangeUnion FromInequation(Inequation<Point> inequation) =>
            new RangeUnion(Implementation.ToOpenRanges<Point, Range, Points, Ranges>(inequation));

        public override bool Equals(object obj) =>
            obj is RangeUnion other && _ranges.SequenceEqual(other._ranges);

        public override int GetHashCode() =>
            _ranges.Aggregate(new HashCode(), (a, b) => { a.Add(b); return a; }, b => b.ToHashCode());

        public Inequation ToInequation() =>
            _ranges
                .Select(x => x.ToInequation())
                .DefaultIfEmpty(Inequation.False())
                .Aggregate((a, b) => Inequation.Or(a, b));

        public override string ToString() => string.Join(" ∪ ", _ranges);
    }
}