using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Inequations
{
    internal sealed class OpenIntervalsInInequation<T, TInterval, TPoints, TIntervals> : IEnumerable<TInterval>
        where TIntervals: struct, IOpenIntervals<T, TInterval>
        where TPoints: struct, IPoints<T>
        where T : IComparable<T>
    {
        private readonly IInequation<T> _inequation;

        public OpenIntervalsInInequation(IInequation<T> inequation) => _inequation = inequation;

        public IEnumerator<TInterval> GetEnumerator()
        {
            var points = default(TPoints);
            var boundaries =
                Enumerable.Repeat(points.Minimum(), 1)
                    .Union(_inequation.Boundaries())
                    .Union(Enumerable.Repeat(points.Maximum(), 1));
            var boundaryPoints = boundaries.Select(x => new Point(x, _inequation));
            var intervalEnds = boundaryPoints.SelectMany(x => x.ToIntervalEnds());
            var intervals =
                intervalEnds
                    .Zip(intervalEnds.Skip(1), (a, b) => a.ToIntervals(b))
                    .SelectMany(x => x);
            return intervals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly struct Point
        {
            private readonly T _value;
            private readonly IInequation<T> _inequation;

            public Point(T value, IInequation<T> inequation)
            {
                _value = value;
                _inequation = inequation;
            }

            public IEnumerable<IntervalEnd> ToIntervalEnds()
            {
                var value = _value;
                return (
                    PreviousSatisfiesInequation(),
                    CurrentSatisfiesInequation(),
                    NextSatisfiesInequation()) switch
                {
                    (false, false, true) => One(OpenStart()),
                    (false, true, false) => Two(ClosedStart(), ClosedEnd()),
                    (false, true, true) => One(ClosedStart()),
                    (true, false, false) => One(OpenEnd()),
                    (true, false, true) => Two(OpenEnd(), OpenStart()),
                    (true, true, false) => One(ClosedEnd()),
                    _ => None(),
                };

                IntervalEnd OpenStart() => new IntervalEnd(value, false, true);
                IntervalEnd OpenEnd() => new IntervalEnd(value, false, false);
                IntervalEnd ClosedStart() => new IntervalEnd(value, true, true);
                IntervalEnd ClosedEnd() => new IntervalEnd(value, true, false);

                static IEnumerable<IntervalEnd> None() => Enumerable.Empty<IntervalEnd>();
                static IEnumerable<IntervalEnd> One(IntervalEnd value) => Enumerable.Repeat(value, 1);
                static IEnumerable<IntervalEnd> Two(IntervalEnd first, IntervalEnd second) => new[] { first, second };
            }

            private bool CurrentSatisfiesInequation() =>
                _inequation.IsSatisfiedBy(_value);

            private bool PreviousSatisfiesInequation() =>
                default(TPoints).Previous(_value) is (true, T previous)
                && _inequation.IsSatisfiedBy(previous);

            private bool NextSatisfiesInequation() =>
                default(TPoints).Next(_value) is (true, T next)
                && _inequation.IsSatisfiedBy(next);

            public override string ToString()
            {
                var points = default(TPoints);
                var previous = points.Previous(_value);
                var next = points.Next(_value);
                return $"{(previous.HasValue ? previous.Value.ToString() : "null")}: {PreviousSatisfiesInequation()}," +
                    $" {_value}: {CurrentSatisfiesInequation()}," +
                    $" {(next.HasValue ? next.Value.ToString() : "null)")}: {NextSatisfiesInequation()}";
            }
        }

        private readonly struct IntervalEnd
        {
            private readonly T _value;
            private readonly Side _side;
            private readonly Kind _kind;

            public IntervalEnd(T value, bool isClosed, bool isStart)
                : this(
                    value,
                    isStart ? Side.Start : Side.End,
                    isClosed ? Kind.Closed : Kind.Open)
            {
            }

            private IntervalEnd(T value, Side side, Kind kind)
            {
                _value = value;
                _side = side;
                _kind = kind;
            }

            public IEnumerable<TInterval> ToIntervals(in IntervalEnd next)
            {
                var intervals = default(TIntervals);
                var points = default(TPoints);
                var first = _value;
                var second = next._value;
                var self = this;
                var other = next;
                var afterFirst = points.Next(first);
                return (_side, next._side) switch
                {
                    (Side.End, Side.Start) => None(),
                    (Side.Start, Side.End)
                        when first.CompareTo(second) == 0 =>
                        (_kind, next._kind) switch
                        {
                            (Kind.Closed, Kind.Closed) => One(ClosedInterval()),
                            _ => None()
                        },
                    (Side.Start, Side.End)
                        when afterFirst.HasValue && afterFirst.Value.CompareTo(second) == 0 =>
                        (_kind, next._kind) switch
                        {
                            (Kind.Closed, Kind.Closed) => One(ClosedInterval()),
                            (Kind.Open, Kind.Closed) => One(LeftOpenInterval()),
                            (Kind.Closed, Kind.Open) => One(RightOpenInterval()),
                            _ => None()
                        },
                    (Side.Start, Side.End) =>
                        (_kind, next._kind) switch
                        {
                            (Kind.Closed, Kind.Closed) => One(ClosedInterval()),
                            (Kind.Closed, Kind.Open) => One(RightOpenInterval()),
                            (Kind.Open, Kind.Open) => One(OpenInterval()),
                            (Kind.Open, Kind.Closed) => One(LeftOpenInterval()),
                            _ => ThrowInvalidStateException()
                        },
                    _ => ThrowInvalidStateException()
                };

                TInterval ClosedInterval() => intervals.NonEmpty(first, second, isStartClosed: true, isEndClosed: true);
                TInterval LeftOpenInterval() => intervals.NonEmpty(first, second, isStartClosed: false, isEndClosed: true);
                TInterval RightOpenInterval() => intervals.NonEmpty(first, second, isStartClosed: true, isEndClosed: false);
                TInterval OpenInterval() => intervals.NonEmpty(first, second, isStartClosed: false, isEndClosed: false);

                static IEnumerable<TInterval> None() => Enumerable.Empty<TInterval>();
                static IEnumerable<TInterval> One(TInterval value) => Enumerable.Repeat(value, 1);
                IEnumerable<TInterval> ThrowInvalidStateException()
                {
                    throw new Exception($"Invalid state, First: `{self.ToString()}`, Second: `{other.ToString()}`");
                }
            }

            public override string ToString() => $"{_value} ({_side}, {_kind})";

            private enum Kind
            {
                Open,
                Closed
            }

            private enum Side
            {
                Start,
                End,
            }
        }
    }
}
