using System;
using System.Collections.Immutable;
using System.Linq;

namespace Mors.Intervals.Operations.Test
{
    public readonly struct IntervalUnion<TInterval> : IEquatable<IntervalUnion<TInterval>>
    {
        private readonly ImmutableArray<TInterval> _intervals;

        public IntervalUnion(ImmutableArray<TInterval> intervals) =>
            _intervals = intervals;

        public static IntervalUnion<TInterval> Empty() =>
            new IntervalUnion<TInterval>(ImmutableArray<TInterval>.Empty);

        public bool Equals(IntervalUnion<TInterval> other) => 
            _intervals.SequenceEqual(other._intervals);

        public override bool Equals(object? obj) => 
            obj is IntervalUnion<TInterval> other && Equals(other);

        public override int GetHashCode() =>
            _intervals.Aggregate(
                new HashCode(),
                (a, b) => { a.Add(b); return a; },
                b => b.ToHashCode());

        public override string ToString() =>
            string.Join(", ", _intervals.Select(x => x.ToString()).DefaultIfEmpty("{empty}"));
    }
}