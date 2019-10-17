using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Inequations.Tests
{
    public readonly struct OpenRangeUnion
    {
        private readonly IEnumerable<OpenRange> _ranges;

        private OpenRangeUnion(IEnumerable<OpenRange> ranges) => _ranges = ranges;

        public static implicit operator OpenRangeUnion(OpenRange range) => FromRange(range);

        public static OpenRangeUnion FromRange(OpenRange range) => new OpenRangeUnion(Enumerable.Repeat(range, 1));

        public static OpenRangeUnion FromInequation(Inequation<Point> inequation) =>
            new OpenRangeUnion(inequation.ToOpenRanges<OpenRange, Points, OpenRanges>());

        public override bool Equals(object obj) =>
            obj is OpenRangeUnion other && _ranges.SequenceEqual(other._ranges);

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