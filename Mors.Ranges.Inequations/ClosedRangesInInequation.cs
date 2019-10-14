using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Mors.Ranges.Inequations
{
    internal sealed class ClosedRangesInInequation<T, TRange, TPoints, TRanges> : IEnumerable<TRange>
        where TRanges : struct, IClosedRanges<T, TRange>
        where TPoints : struct, IPoints<T>
        where T : IComparable<T>
    {
        private readonly IInequation<T> _inequation;

        public ClosedRangesInInequation(IInequation<T> inequation) => _inequation = inequation;

        public IEnumerator<TRange> GetEnumerator()
        {
            var boundaries =
                Enumerable.Repeat(default(TPoints).Minimum(), 1)
                    .Union(_inequation.Boundaries())
                    .Union(Enumerable.Repeat(default(TPoints).Maximum(), 1));
            var boundaryPoints =
                boundaries.Select(x => new BoundaryPoint(x));
            var points =
                boundaryPoints
                    .SelectMany(x => x.ToPoints())
                    .ToImmutableSortedSet();
            var union =
                points.Aggregate(
                    new RangeUnion(),
                    (a, b) => a.AppendPoint(b, b.SatisfiesInequation(_inequation)));
            var ranges = union.ToRanges();
            return ranges.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly struct RangeUnion
        {
            private readonly ImmutableArray<TRange> _ranges;
            private readonly (Point Start, Point? End)? _newRange;

            private RangeUnion(
                ImmutableArray<TRange> ranges,
                (Point Start, Point? End)? newRange) =>
                (_ranges, _newRange) = (ranges, newRange);

            public RangeUnion AppendPoint(Point point, bool pointSatisfiesInequation) =>
                (_newRange, pointSatisfiesInequation) switch
                {
                    (null, false) => this,
                    (null, true) => WithNewRange(start: point, end: null),
                    ((Point start, null), false) => WithRangeAndNoNewRange(start.ToRange(start)),
                    ((Point start, null), true) => WithNewRange(start, end: point),
                    ((Point start, Point end), false) => WithRangeAndNoNewRange(start.ToRange(end)),
                    ((Point start, Point _), true) => WithNewRange(start, point),
                };

            public IEnumerable<TRange> ToRanges() =>
                (_ranges.IsDefault ? ImmutableArray<TRange>.Empty : _ranges)
                    .AddRange(
                        _newRange is (Point start, Point end)
                            ? Enumerable.Repeat(start.ToRange(end), 1)
                            : Enumerable.Empty<TRange>())
                    .DefaultIfEmpty(default(TRanges).Empty());

            public override string ToString() =>
                $"New range: `{(_newRange?.ToString() ?? "none")}`, Ranges: {_ranges.Length}";

            private RangeUnion WithNewRange(Point start, Point? end) =>
                new RangeUnion(_ranges, (start, end));

            private RangeUnion WithRangeAndNoNewRange(TRange range) =>
                new RangeUnion(
                    _ranges.IsDefault
                        ? ImmutableArray.Create(range)
                        : _ranges.Add(range),
                    null);
        }

        private readonly struct BoundaryPoint
        {
            private readonly T _value;

            public BoundaryPoint(in T value) => _value = value;

            public IEnumerable<Point> ToPoints() =>
                (default(TPoints).Previous(_value),
                 default(TPoints).Next(_value)) switch
                {
                    ((true, T x), (true, T y)) => new[] { new Point(x), new Point(_value), new Point(y) },
                    ((false, _), (true, T y)) => new[] { new Point(_value), new Point(y) },
                    ((true, T x), (false, _)) => new[] { new Point(x), new Point(_value) },
                    _ => new[] { new Point(_value) }
                };
        }

        private readonly struct Point : IEquatable<Point>, IComparable<Point>
        {
            private readonly T _value;

            public Point(T value) => _value = value;

            public int CompareTo(Point other) => _value.CompareTo(other._value);

            public bool Equals(Point other) => _value.CompareTo(other._value) == 0;

            public override bool Equals(object obj) => obj is Point other && Equals(other);

            public override int GetHashCode() =>
                -1939223833 + EqualityComparer<T>.Default.GetHashCode(_value);

            public bool SatisfiesInequation(IInequation<T> inequation) =>
                inequation.IsSatisfiedBy(_value);

            public TRange ToRange(in Point other) =>
                _value.CompareTo(other._value) <= 0
                    ? default(TRanges).NonEmpty(_value, other._value)
                    : throw new Exception($"A range start {_value} must be lower than or equal to a range end {other._value}");

            public override string ToString() => _value.ToString();
        }
    }
}
