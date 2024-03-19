using System;
using System.Collections.Immutable;
using System.Linq;

namespace Mors.Intervals.Operations.Test
{
    public readonly struct OpenIntervalUnion : IEquatable<OpenIntervalUnion>
    {
        private readonly ImmutableArray<OpenInterval> _intervals;

        public OpenIntervalUnion(ImmutableArray<OpenInterval> intervals) =>
            _intervals = intervals;

        public static OpenIntervalUnion Empty() =>
            new OpenIntervalUnion(ImmutableArray<OpenInterval>.Empty);

        public bool Equals(OpenIntervalUnion other) => 
            _intervals.SequenceEqual(other._intervals);

        public override bool Equals(object? obj) => 
            obj is OpenIntervalUnion other && Equals(other);

        public override int GetHashCode() =>
            _intervals.Aggregate(
                new HashCode(),
                (a, b) => { a.Add(b); return a; },
                b => b.ToHashCode());

        public override string ToString() =>
            string.Join(", ", _intervals.Select(x => x.ToString()).DefaultIfEmpty("{empty}"));
    }
}