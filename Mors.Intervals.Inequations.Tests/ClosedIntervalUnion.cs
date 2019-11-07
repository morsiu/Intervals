using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Inequations.Tests
{
    public readonly struct ClosedIntervalUnion
    {
        private readonly IEnumerable<ClosedInterval> _intervals;

        private ClosedIntervalUnion(IEnumerable<ClosedInterval> intervals) => _intervals = intervals;

        public static implicit operator ClosedIntervalUnion(ClosedInterval interval) => FromInterval(interval);

        public static ClosedIntervalUnion FromInterval(ClosedInterval interval) =>
            new ClosedIntervalUnion(
                interval.IsEmpty
                    ? Enumerable.Empty<ClosedInterval>()
                    : Enumerable.Repeat(interval, 1));

        public static ClosedIntervalUnion FromInequation(Inequation<Point> inequation) =>
            new ClosedIntervalUnion(inequation.ToClosedIntervals<ClosedInterval, Points, ClosedIntervals>());

        public override bool Equals(object obj) =>
            obj is ClosedIntervalUnion other && _intervals.SequenceEqual(other._intervals);

        public override int GetHashCode() =>
            _intervals.Aggregate(new HashCode(), (a, b) => { a.Add(b); return a; }, b => b.ToHashCode());

        public Inequation ToInequation() =>
            _intervals
                .Select(x => x.ToInequation())
                .DefaultIfEmpty(Inequation.False())
                .Aggregate((a, b) => Inequation.Or(a, b));

        public override string ToString() =>
            string.Join(" ∪ ", _intervals.Select(x => x.ToString()).DefaultIfEmpty("∅"));
    }
}