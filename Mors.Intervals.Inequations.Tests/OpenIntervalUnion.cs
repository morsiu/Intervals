using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Inequations.Tests
{
    public readonly struct OpenIntervalUnion
    {
        private readonly IEnumerable<OpenInterval> _intervals;

        private OpenIntervalUnion(IEnumerable<OpenInterval> intervals) => _intervals = intervals;

        public static implicit operator OpenIntervalUnion(OpenInterval interval) => FromInterval(interval);

        public static OpenIntervalUnion FromInterval(OpenInterval interval) =>
            new OpenIntervalUnion(
                interval.IsEmpty()
                    ? Enumerable.Empty<OpenInterval>()
                    : Enumerable.Repeat(interval, 1));

        public static OpenIntervalUnion FromInequation(Inequation<Point> inequation) =>
            new OpenIntervalUnion(inequation.ToOpenIntervals<OpenInterval, Points, OpenIntervals>());

        public override bool Equals(object? obj) =>
            obj is OpenIntervalUnion other && _intervals.SequenceEqual(other._intervals);

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