using System;

namespace Mors.Intervals.Operations
{
    public static class OpenIntervalOperations
    {
        public static bool Contains<TPoint, TInterval>(
            in TInterval interval,
            in TPoint point)
            where TPoint : IComparable<TPoint>
            where TInterval : IOpenInterval<TPoint>
        {
            var pointToStart = point.CompareTo(interval.Start);
            var pointToEnd = point.CompareTo(interval.End);
            if (pointToStart < 0) return false;
            if (pointToStart == 0) return pointToEnd != 0 ? !interval.OpenStart : !interval.OpenStart && !interval.OpenEnd;
            if (pointToEnd < 0) return true;
            if (pointToEnd == 0) return !interval.OpenEnd;
            return false;
        }

        public static bool Covers<TPoint, TInterval>(
            in TInterval left,
            in TInterval right)
            where TPoint : IComparable<TPoint>
            where TInterval : IOpenInterval<TPoint>
        {
            if (!IntersectsWith<TPoint, TInterval>(left, right))
                return false;

            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);

            return (leftStartToRightStart < 0 || (leftStartToRightStart == 0 && (!left.OpenStart || right.OpenStart)))
                && (leftEndToRightEnd > 0 || (leftEndToRightEnd  == 0 && (!left.OpenEnd || right.OpenEnd)));
        }
        
        public static bool IsCoveredBy<TPoint, TInterval>(
            in TInterval left,
            in TInterval right)
            where TPoint : IComparable<TPoint>
            where TInterval : IOpenInterval<TPoint>
        {
            return Covers<TPoint, TInterval>(right, left);
        }
        
        public static void Intersect<TPoint, TInterval, TIntervals>(
            in TInterval left,
            in TInterval right,
            out TInterval result)
            where TPoint : IComparable<TPoint>
            where TInterval : IOpenInterval<TPoint>, IEmptyInterval
            where TIntervals : struct, IOpenIntervals<TPoint, TInterval>, IEmptyIntervals<TInterval>
        {
            if (!IntersectsWith<TPoint, TInterval>(left, right))
            {
                result = default(TIntervals).Empty();
                return;
            }
            
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            
            result = default(TIntervals).Interval(
                leftStartToRightStart > 0 ? left.Start : right.Start,
                leftEndToRightEnd < 0 ? left.End : right.End,
                leftStartToRightStart == 0
                    ? left.OpenStart || right.OpenStart
                    : leftStartToRightStart > 0
                        ? left.OpenStart
                        : right.OpenStart,
                leftEndToRightEnd == 0
                    ? left.OpenEnd || right.OpenEnd
                    : leftEndToRightEnd < 0
                        ? left.OpenEnd
                        : right.OpenEnd);
        }
        
        public static bool IntersectsWith<TPoint, TInterval>(
            in TInterval left,
            in TInterval right)
            where TPoint : IComparable<TPoint>
            where TInterval : IOpenInterval<TPoint>
        {
            var leftStartToRightEnd = left.Start.CompareTo(right.End);
            
            if (leftStartToRightEnd > 0) return false;
            
            var leftEndToRightStart = left.End.CompareTo(right.Start);
            
            if (leftEndToRightStart < 0) return false;
            if (leftStartToRightEnd == 0) return !left.OpenStart && !right.OpenEnd;
            if (leftEndToRightStart == 0) return !left.OpenEnd && !right.OpenStart;
            return true;
        }
        
        public static void Span<TPoint, TInterval, TIntervals>(
            in TInterval left,
            in TInterval right,
            out TInterval result)
            where TPoint : IComparable<TPoint>
            where TInterval : IOpenInterval<TPoint>
            where TIntervals : struct, IOpenIntervals<TPoint, TInterval>
        {
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            
            result = default(TIntervals).Interval(
                leftStartToRightStart < 0 ? left.Start : right.Start,
                leftEndToRightEnd > 0 ? left.End : right.End,
                leftStartToRightStart == 0
                    ? left.OpenStart && right.OpenStart
                    : leftStartToRightStart < 0
                        ? left.OpenStart
                        : right.OpenStart,
                leftEndToRightEnd == 0
                    ? left.OpenEnd && right.OpenEnd
                    : leftEndToRightEnd > 0
                        ? left.OpenEnd
                        : right.OpenEnd);
        }

        public static void Subtract<TPoint, TInterval, TIntervalUnion, TIntervals, TIntervalUnions>(
            in TInterval left,
            in TInterval right,
            out TIntervalUnion result)
            where TPoint : IComparable<TPoint>
            where TInterval : IOpenInterval<TPoint>
            where TIntervals : struct, IOpenIntervals<TPoint, TInterval>
            where TIntervalUnions : struct, IIntervalUnions<TInterval, TIntervalUnion>
        {
            var leftStartToRightEnd = left.Start.CompareTo(right.End);
            if (leftStartToRightEnd > 0)
            {
                // (BI) right before left
                result = default(TIntervalUnions).NonEmpty(left);
                return;
            }
            if (leftStartToRightEnd == 0)
            {
                // (MI) right meets left
                result = default(TIntervalUnions).NonEmpty(
                    default(TIntervals).Interval(left.Start, left.End, left.OpenStart || !right.OpenEnd, left.OpenEnd));
                return;
            }
            var leftEndToRightStart = left.End.CompareTo(right.Start);
            if (leftEndToRightStart == 0)
            {
                // (M) left meets right
                result = default(TIntervalUnions).NonEmpty(
                    default(TIntervals).Interval(left.Start, left.End, left.OpenStart, left.OpenEnd || !right.OpenStart));
                return;
            }
            if (leftEndToRightStart < 0)
            {
                // (B) left before right
                result = default(TIntervalUnions).NonEmpty(left);
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            if (leftStartToRightStart > 0)
            {
                if (leftEndToRightEnd > 0)
                {
                    // (OI) right overlaps left
                    result = default(TIntervalUnions).NonEmpty(
                        default(TIntervals).Interval(right.End, left.End, !right.OpenEnd, left.OpenEnd));
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (F) left finishes right
                    result = !left.OpenEnd && right.OpenEnd
                        ? default(TIntervalUnions).NonEmpty(
                            default(TIntervals).Interval(left.End, left.End, openStart: false, openEnd: false))
                        : default(TIntervalUnions).Empty();
                }
                else
                {
                    // (D) left during right
                    result = default(TIntervalUnions).Empty();
                }
            }
            else if (leftStartToRightStart == 0)
            {
                if (leftEndToRightEnd > 0)
                {
                    // (SI) right starts left
                    var second = default(TIntervals).Interval(right.End, left.End, !right.OpenEnd, left.OpenEnd);
                    result = !left.OpenStart && right.OpenStart
                        ? default(TIntervalUnions).NonEmpty(
                            default(TIntervals).Interval(left.Start, left.Start, openStart: false, openEnd: false),
                            second)
                        : default(TIntervalUnions).NonEmpty(second);
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (EQ) left equals right
                    var startIncluded = !left.OpenStart && right.OpenStart;
                    var endIncluded = !left.OpenEnd && right.OpenEnd;
                    result = (startIncluded, endIncluded) switch
                    {
                        (true, true) => default(TIntervalUnions).NonEmpty(
                            default(TIntervals).Interval(left.Start, left.Start, openStart: false, openEnd: false),
                            default(TIntervals).Interval(left.End, left.End, openStart: false, openEnd: false)),
                        (true, false) => default(TIntervalUnions).NonEmpty(
                            default(TIntervals).Interval(left.Start, left.Start, openStart: false, openEnd: false)),
                        (false, true) => default(TIntervalUnions).NonEmpty(
                            default(TIntervals).Interval(left.End, left.End, openStart: false, openEnd: false)),
                        _ => default(TIntervalUnions).Empty()
                    };
                }
                else
                {
                    // (S) left starts right
                    result = !left.OpenStart && right.OpenStart
                        ? default(TIntervalUnions).NonEmpty(
                            default(TIntervals).Interval(left.Start, left.Start, openStart: false, openEnd: false))
                        : default(TIntervalUnions).Empty();
                }
            }
            else
            {
                if (leftEndToRightEnd > 0)
                {
                    // (DI) right during first
                    result = default(TIntervalUnions).NonEmpty(
                        default(TIntervals).Interval(left.Start, right.Start, left.OpenStart, !right.OpenStart),
                        default(TIntervals).Interval(right.End, left.End, !right.OpenEnd, left.OpenEnd));
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (FI) right finishes left
                    var first = default(TIntervals).Interval(left.Start, right.Start, left.OpenStart, !right.OpenStart);
                    result = !left.OpenEnd && right.OpenEnd
                        ? default(TIntervalUnions).NonEmpty(first,
                            default(TIntervals).Interval(left.End, left.End, openStart: false, openEnd: false))
                        : default(TIntervalUnions).NonEmpty(first);
                }
                else
                {
                    // (O) left overlaps right
                    result = default(TIntervalUnions).NonEmpty(
                        default(TIntervals).Interval(left.Start, right.Start, left.OpenStart, !right.OpenStart));
                }
            }
        }

        public static void Union<TPoint, TInterval, TIntervalUnion, TIntervals, TIntervalUnions>(
            in TInterval left,
            in TInterval right,
            out TIntervalUnion result)
            where TPoint : IComparable<TPoint>
            where TInterval : IOpenInterval<TPoint>
            where TIntervals : struct, IOpenIntervals<TPoint, TInterval>
            where TIntervalUnions : struct, IIntervalUnions<TInterval, TIntervalUnion>
        {
            var leftStartToRightEnd = left.Start.CompareTo(right.End);
            if (leftStartToRightEnd > 0
                || (leftStartToRightEnd == 0 && left.OpenStart && right.OpenEnd))
            {
                result = default(TIntervalUnions).NonEmpty(right, left);
                return;
            }
            var leftEndToRightStart = left.End.CompareTo(right.Start);
            if (leftEndToRightStart < 0
                || (leftEndToRightStart == 0 && left.OpenEnd && right.OpenStart))
            {
                result = default(TIntervalUnions).NonEmpty(left, right);
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            result = default(TIntervalUnions).NonEmpty(
                default(TIntervals).Interval(
                    leftStartToRightStart < 0
                        ? left.Start
                        : right.Start,
                    leftEndToRightEnd > 0
                        ? left.End
                        : right.End,
                    leftStartToRightStart switch
                    {
                        var x when x < 0 => left.OpenStart,
                        var x when x > 0 => right.OpenStart,
                        _ => left.OpenStart && right.OpenStart
                    },
                    leftEndToRightEnd switch
                    {
                        var x when x < 0 => right.OpenEnd,
                        var x when x > 0 => left.OpenEnd,
                        _ => left.OpenEnd && right.OpenEnd
                    }));
        }
    }
}