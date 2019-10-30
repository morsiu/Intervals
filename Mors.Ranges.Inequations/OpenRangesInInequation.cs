using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Inequations
{
    internal sealed class OpenRangesInInequation<T, TRange, TPoints, TRanges> : IEnumerable<TRange>
        where TRanges: struct, IOpenRanges<T, TRange>
        where TPoints: struct, IPoints<T>
        where T : IComparable<T>
    {
        private readonly IInequation<T> _inequation;

        public OpenRangesInInequation(IInequation<T> inequation) => _inequation = inequation;

        public IEnumerator<TRange> GetEnumerator()
        {
            var points = default(TPoints);
            var boundaries =
                Enumerable.Repeat(points.Minimum(), 1)
                    .Union(_inequation.Boundaries())
                    .Union(Enumerable.Repeat(points.Maximum(), 1));
            var boundaryPoints = boundaries.Select(x => new Point(x, _inequation));
            var rangeEnds = boundaryPoints.SelectMany(x => x.ToRangeEnds());
            var ranges =
                rangeEnds
                    .Zip(rangeEnds.Skip(1), (a, b) => a.ToRanges(b))
                    .SelectMany(x => x);
            return ranges.GetEnumerator();
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

            public IEnumerable<RangeEnd> ToRangeEnds()
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

                RangeEnd OpenStart() => new RangeEnd(value, false, true);
                RangeEnd OpenEnd() => new RangeEnd(value, false, false);
                RangeEnd ClosedStart() => new RangeEnd(value, true, true);
                RangeEnd ClosedEnd() => new RangeEnd(value, true, false);

                static IEnumerable<RangeEnd> None() => Enumerable.Empty<RangeEnd>();
                static IEnumerable<RangeEnd> One(RangeEnd value) => Enumerable.Repeat(value, 1);
                static IEnumerable<RangeEnd> Two(RangeEnd first, RangeEnd second) => new[] { first, second };
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

        private readonly struct RangeEnd
        {
            private readonly T _value;
            private readonly Side _side;
            private readonly Kind _kind;

            public RangeEnd(T value, bool isClosed, bool isStart)
                : this(
                    value,
                    isStart ? Side.Start : Side.End,
                    isClosed ? Kind.Closed : Kind.Open)
            {
            }

            private RangeEnd(T value, Side side, Kind kind)
            {
                _value = value;
                _side = side;
                _kind = kind;
            }

            public IEnumerable<TRange> ToRanges(in RangeEnd next)
            {
                var ranges = default(TRanges);
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
                            (Kind.Closed, Kind.Closed) => One(ClosedRange()),
                            _ => None()
                        },
                    (Side.Start, Side.End)
                        when afterFirst.HasValue && afterFirst.Value.CompareTo(second) == 0 =>
                        (_kind, next._kind) switch
                        {
                            (Kind.Closed, Kind.Closed) => One(ClosedRange()),
                            (Kind.Open, Kind.Closed) => One(LeftOpenRange()),
                            (Kind.Closed, Kind.Open) => One(RightOpenRange()),
                            _ => None()
                        },
                    (Side.Start, Side.End) =>
                        (_kind, next._kind) switch
                        {
                            (Kind.Closed, Kind.Closed) => One(ClosedRange()),
                            (Kind.Closed, Kind.Open) => One(RightOpenRange()),
                            (Kind.Open, Kind.Open) => One(OpenRange()),
                            (Kind.Open, Kind.Closed) => One(LeftOpenRange()),
                            _ => ThrowInvalidStateException()
                        },
                    _ => ThrowInvalidStateException()
                };

                TRange ClosedRange() => ranges.NonEmpty(first, second, isStartClosed: true, isEndClosed: true);
                TRange LeftOpenRange() => ranges.NonEmpty(first, second, isStartClosed: false, isEndClosed: true);
                TRange RightOpenRange() => ranges.NonEmpty(first, second, isStartClosed: true, isEndClosed: false);
                TRange OpenRange() => ranges.NonEmpty(first, second, isStartClosed: false, isEndClosed: false);

                static IEnumerable<TRange> None() => Enumerable.Empty<TRange>();
                static IEnumerable<TRange> One(TRange value) => Enumerable.Repeat(value, 1);
                IEnumerable<TRange> ThrowInvalidStateException()
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
