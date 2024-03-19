using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Mors.Intervals.Operations.Test
{
    public readonly struct ClosedIntervalUnion
        : IEquatable<ClosedIntervalUnion>,
          Reference.IClosedIntervalUnion<int, ClosedInterval>,
          IClosedIntervalUnion<ClosedInterval, IEnumerator<ClosedInterval>, int>
    {
        private readonly ImmutableArray<ClosedInterval> _intervals;

        public ClosedIntervalUnion(ImmutableArray<ClosedInterval> intervals) =>
            _intervals = intervals;

        public static ClosedIntervalUnion Empty() =>
            new ClosedIntervalUnion(ImmutableArray<ClosedInterval>.Empty);

        public bool Equals(ClosedIntervalUnion other) => 
            _intervals.SequenceEqual(other._intervals);

        public override bool Equals(object? obj) => 
            obj is ClosedIntervalUnion other && Equals(other);

        public IEnumerator<ClosedInterval> GetEnumerator() => ((IEnumerable<ClosedInterval>)_intervals).GetEnumerator();

        public override int GetHashCode() =>
            _intervals.Aggregate(
                new HashCode(),
                (a, b) => { a.Add(b); return a; },
                b => b.ToHashCode());

        public override string ToString() =>
            "[" + string.Join(", ", _intervals.Select(x => x.ToString())) + "]";

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_intervals).GetEnumerator();
    }
}