using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Mors.Intervals.Operations
{
    public static class ClosedIntervalUnionOperations
    {
        public static TIntervalUnion Intersect<
            TIntervalUnion,
            TIntervalEnumerator,
            TInterval,
            TPoint,
            TIntervalUnionBuilder,
            TIntervals>(
            TIntervalUnion left,
            TIntervalUnion right)
            where TIntervalUnion : IClosedIntervalUnion<TInterval, TIntervalEnumerator, TPoint>
            where TIntervalEnumerator : IEnumerator<TInterval>
            where TIntervalUnionBuilder : struct, IIntervalUnionBuilder<TIntervalUnion, TInterval>
            where TIntervals : struct, IClosedIntervals<TPoint, TInterval>
            where TInterval : struct, IClosedInterval<TPoint>
            where TPoint : IComparable<TPoint>
        {
            using var leftInnerEnumerator = left.GetEnumerator();
            using var rightInnerEnumerator = right.GetEnumerator();
            var leftEnumerator = leftInnerEnumerator;
            var rightEnumerator = rightInnerEnumerator;
            var result = default(TIntervalUnionBuilder);
            if (!leftEnumerator.MoveNext() || !rightEnumerator.MoveNext())
            {
                return result.Build();
            }
            var comparer = Comparer<TPoint>.Default;
            do
            {
                var leftValue = leftEnumerator.Current;
                var rightValue = rightEnumerator.Current;
                var leftIsFirst = comparer.Compare(leftValue.Start, rightValue.Start) < 0;
                ref var first = ref leftEnumerator;
                ref var second = ref rightEnumerator;
                ref var firstValue = ref leftValue;
                ref var secondValue = ref rightValue;
                if (!leftIsFirst)
                {
                    first = ref rightEnumerator;
                    second = ref leftEnumerator;
                    firstValue = ref rightValue;
                    secondValue = ref leftValue;
                }
                if (comparer.Compare(firstValue.End, secondValue.Start) < 0)
                {
                    if (!first.MoveNext())
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                var intersectionStart = secondValue.Start;
                var firstFinishesFirst = comparer.Compare(firstValue.End, secondValue.End) < 0;
                var intersectionEnd =
                    firstFinishesFirst
                        ? firstValue.End
                        : secondValue.End;
                result.Append(default(TIntervals).Interval(intersectionStart, intersectionEnd));
                if (firstFinishesFirst)
                {
                    if (!first.MoveNext())
                    {
                        break;
                    }
                }
                else
                {
                    if (!second.MoveNext())
                    {
                        break;
                    }
                }
            }
            while (true);
            return result.Build();
        }

        public static TIntervalUnion Union<
            TIntervalUnion,
            TIntervalEnumerator,
            TInterval,
            TPoint,
            TPoints,
            TIntervalUnionBuilder,
            TIntervals>(
            TIntervalUnion left,
            TIntervalUnion right)
            where TIntervalUnion : IClosedIntervalUnion<TInterval, TIntervalEnumerator, TPoint>
            where TIntervalEnumerator : IEnumerator<TInterval>
            where TIntervalUnionBuilder : struct, IIntervalUnionBuilder<TIntervalUnion, TInterval>
            where TIntervals : struct, IClosedIntervals<TPoint, TInterval>
            where TInterval : struct, IClosedInterval<TPoint>
            where TPoints : struct, IPoints<TPoint>
            where TPoint : IComparable<TPoint>
        {
            using var leftInnerEnumerator = left.GetEnumerator();
            using var rightInnerEnumerator = right.GetEnumerator();
            var leftEnumerator = new Enumerator<TInterval, TIntervalEnumerator>(leftInnerEnumerator);
            var rightEnumerator = new Enumerator<TInterval, TIntervalEnumerator>(rightInnerEnumerator);
            if (!leftEnumerator.MoveNext())
            {
                return right;
            }
            if (!rightEnumerator.MoveNext())
            {
                return left;
            }
            var result = default(TIntervalUnionBuilder);
            var leftValue = leftEnumerator.Current;
            var rightValue = rightEnumerator.Current;
            do
            {
                var leftIsFirst =
                    Comparer<TPoint>.Default.Compare(leftValue.Start, rightValue.Start) < 0;
                ref var first = ref leftEnumerator;
                ref var second = ref rightEnumerator;
                ref var firstValue = ref leftValue;
                ref var secondValue = ref rightValue;
                if (!leftIsFirst)
                {
                    first = ref rightEnumerator;
                    second = ref leftEnumerator;
                    firstValue = ref rightValue;
                    secondValue = ref leftValue;
                }
                if (TryIntersect(firstValue, secondValue, ref firstValue))
                {
                    MergeValueWithCurrentWhileBothIntersect(ref first, ref firstValue);
                    if (!second.MoveNext())
                    {
                        result.Append(firstValue);
                        AppendWithoutCurrent(ref first, ref result);
                        return result.Build();
                    }
                    secondValue = second.Current;
                }
                else
                {
                    result.Append(firstValue);
                    if (!first.MoveNext())
                    {
                        result.Append(secondValue);
                        AppendWithoutCurrent(ref second, ref result);
                        return result.Build();
                    }
                    firstValue = first.Current;
                }
            }
            while (true);

            static void AppendWithoutCurrent(
                ref Enumerator<TInterval, TIntervalEnumerator> enumerator,
                ref TIntervalUnionBuilder builder)
            {
                while (enumerator.MoveNext())
                {
                    builder.Append(enumerator.Current);
                }
            }

            static bool TryIntersect(
                in TInterval first,
                in TInterval second,
                ref TInterval output)
            {
                Debug.Assert(Comparer<TPoint>.Default.Compare(first.Start, second.Start) <= 0);
                var firstEndToSecondStart = Comparer<TPoint>.Default.Compare(first.End, second.Start);
                var intervalsIntersect =
                    firstEndToSecondStart switch
                    {
                        < 0 => Comparer<TPoint>.Default.Compare(
                                first.End,
                                default(TPoints).UnsafePrevious(second.Start))
                            == 0,
                        0 or > 0 => true,
                    };
                if (intervalsIntersect)
                {
                    output =
                        default(TIntervals).Interval(
                            first.Start,
                            Comparer<TPoint>.Default.Compare(first.End, second.End) > 0
                                ? first.End
                                : second.End);
                    return true;
                }
                return false;
            }

            static void MergeValueWithCurrentWhileBothIntersect(
                ref Enumerator<TInterval, TIntervalEnumerator> enumerator,
                ref TInterval value)
            {
                while (enumerator.PeekNext(out var next))
                {
                    if (TryIntersect(value, next, ref value))
                    {
                        _ = enumerator.MoveNext();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private struct Enumerator<TInterval, TIntervalEnumerator>(TIntervalEnumerator inner)
            where TIntervalEnumerator : IEnumerator<TInterval>
        {
            private State _state;

            public TInterval Current { get; private set; }

            public bool MoveNext()
            {
                switch (_state)
                {
                    case State.NotMoved:
                    case State.AtCurrent:
                        if (inner.MoveNext())
                        {
                            _state = State.AtCurrent;
                            Current = inner.Current;
                            return true;
                        }
                        else
                        {
                            _state = State.AtEnd;
                            return false;
                        }
                    case State.AtNextCurrent:
                        _state = State.AtCurrent;
                        Current = inner.Current;
                        return true;
                    case State.AtNextEnd:
                        _state = State.AtEnd;
                        return false;
                    default:
                        return false;
                }
            }

            public bool PeekNext([MaybeNullWhen(false)] out TInterval next)
            {
                switch (_state)
                {
                    case State.NotMoved:
                    case State.AtCurrent:
                        if (inner.MoveNext())
                        {
                            _state = State.AtNextCurrent;
                            next = inner.Current;
                            return true;
                        }
                        else
                        {
                            _state = State.AtNextEnd;
                            next = default;
                            return false;
                        }
                    case State.AtNextCurrent:
                        next = inner.Current;
                        return true;
                    default:
                        next = default;
                        return false;
                }
            }

            private enum State
            {
                NotMoved,
                AtCurrent,
                AtNextCurrent,
                AtNextEnd,
                AtEnd,
            }
        }
    }
}
