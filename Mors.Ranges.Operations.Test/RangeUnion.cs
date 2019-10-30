using System;
using System.Collections.Immutable;
using System.Linq;

namespace Mors.Ranges.Operations
{
    public readonly struct RangeUnion<TRange> : IEquatable<RangeUnion<TRange>>
    {
        private readonly ImmutableArray<TRange> _ranges;

        public RangeUnion(ImmutableArray<TRange> ranges) =>
            _ranges = ranges;

        public static RangeUnion<TRange> Empty() =>
            new RangeUnion<TRange>(ImmutableArray<TRange>.Empty);

        public bool Equals(RangeUnion<TRange> other) => 
            _ranges.SequenceEqual(other._ranges);

        public override bool Equals(object? obj) => 
            obj is RangeUnion<TRange> other && Equals(other);

        public override int GetHashCode() =>
            _ranges.Aggregate(
                new HashCode(),
                (a, b) => { a.Add(b); return a; },
                b => b.ToHashCode());

        public override string ToString() =>
            string.Join(", ", _ranges.Select(x => x.ToString()).DefaultIfEmpty("{empty}"));
    }
}