using System;

namespace Mors.Intervals.Operations
{
    public static class ClosedIntervalOperations
    {
        public static bool Contains<TPoint, TInterval>(
            in TInterval interval,
            in TPoint point)
            where TPoint : IComparable<TPoint>
            where TInterval : IClosedInterval<TPoint>, IEmptyInterval
        {
            return !interval.Empty
                   && point.CompareTo(interval.Start) >= 0
                   && point.CompareTo(interval.End) <= 0;
        }
        
        public static bool Covers<TPoint, TInterval>(
            in TInterval left,
            in TInterval right)
            where TPoint : IComparable<TPoint>
            where TInterval : IClosedInterval<TPoint>, IEmptyInterval
        {
            if (!IntersectsWith<TPoint, TInterval>(left, right))
                return false;

            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);

            return (leftStartToRightStart < 0 || leftStartToRightStart == 0)
                   && (leftEndToRightEnd > 0 || leftEndToRightEnd  == 0);
        }
        
        public static bool IntersectsWith<TPoint, TInterval>(
            in TInterval left,
            in TInterval right)
            where TPoint : IComparable<TPoint>
            where TInterval : IClosedInterval<TPoint>
        {
            if (left.Start.CompareTo(right.End) > 0) return false;
            if (left.End.CompareTo(right.Start) < 0) return false;
            return true;
        }
        
        public static void Intersect<TPoint, TInterval, TIntervals, TMaybeInterval, TMaybeIntervals>(
            in TInterval left,
            in TInterval right,
            out TInterval result)
            where TPoint : IComparable<TPoint>
            where TInterval : IClosedInterval<TPoint>
            where TIntervals : struct, IClosedIntervals<TPoint, TInterval>
            where TMaybeInterval : IClosedInterval<TPoint>, IEmptyInterval
            where TMaybeIntervals : struct, IClosedIntervals<TPoint, TInterval>, IEmptyIntervals<TInterval>
        {
            if (!IntersectsWith<TPoint, TInterval>(left, right))
            {
                result = default(TMaybeIntervals).Empty();
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            result = default(TMaybeIntervals).Interval(
                leftStartToRightStart > 0 ? left.Start : right.Start,
                leftEndToRightEnd < 0 ? left.End : right.End);
        }
        
        public static bool IsCoveredBy<TPoint, TInterval>(
            in TInterval left,
            in TInterval right)
            where TPoint : IComparable<TPoint>
            where TInterval : IClosedInterval<TPoint>, IEmptyInterval
        {
            return Covers<TPoint, TInterval>(right, left);
        }
        
        public static void Span<TPoint, TInterval, TIntervals>(
            in TInterval left,
            in TInterval right,
            out TInterval result)
            where TPoint : IComparable<TPoint>
            where TInterval : IClosedInterval<TPoint>, IEmptyInterval
            where TIntervals : struct, IClosedIntervals<TPoint, TInterval>
        {
            if (left.Empty)
            {
                result = right;
                return;
            }
            if (right.Empty)
            {
                result = left;
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            result = default(TIntervals).Interval(
                leftStartToRightStart < 0 ? left.Start : right.Start,
                leftEndToRightEnd > 0 ? left.End : right.End);
        }

        public static void Subtract<TPoint, TInterval, TIntervalUnion, TIntervals, TIntervalUnions>(
            in TInterval left,
            in TInterval right,
            IPoints points,
            out TIntervalUnion result)
            where TPoint : IComparable<TPoint>
            where TInterval : IClosedInterval<TPoint>
            where TIntervals : struct, IClosedIntervals<TPoint, TInterval>
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
                    default(TIntervals).Interval(points.For<TPoint>().UnsafeNext(right.End), left.End));
                return;
            }
            var leftEndToRightStart = left.End.CompareTo(right.Start);
            if (leftEndToRightStart == 0)
            {
                // (M) left meets right
                result = default(TIntervalUnions).NonEmpty(
                    default(TIntervals).Interval(left.Start, points.For<TPoint>().UnsafePrevious(right.Start)));
                return;
            }
            if (leftEndToRightStart < 0)
            {
                // (B) left before right
                result = default(TIntervalUnions).NonEmpty(left);
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            if (leftStartToRightStart > 0)
            {
                var leftEndToRightEnd = left.End.CompareTo(right.End);
                if (leftEndToRightEnd < 0)
                {
                    // (D) left during right
                    result = default(TIntervalUnions).Empty();
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (F) left finishes right
                    result = default(TIntervalUnions).Empty();
                }
                else
                {
                    // (OI) right overlaps left
                    result = default(TIntervalUnions).NonEmpty(
                        default(TIntervals).Interval(points.For<TPoint>().UnsafeNext(right.End), left.End));
                }
            }
            else if (leftStartToRightStart == 0)
            {
                var leftEndToRightEnd = left.End.CompareTo(right.End);
                if (leftEndToRightEnd > 0)
                {
                    // (SI) right starts left
                    result = default(TIntervalUnions).NonEmpty(
                        default(TIntervals).Interval(points.For<TPoint>().UnsafeNext(right.End), left.End));
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (EQ) left equals right
                    result = default(TIntervalUnions).Empty();
                }
                else
                {
                    // (S) left starts right
                    result = default(TIntervalUnions).Empty();
                }
            }
            else
            {
                var specificPoints = points.For<TPoint>();
                var beforeRightStart = specificPoints.UnsafePrevious(right.Start);
                var leftEndToRightEnd = left.End.CompareTo(right.End);
                if (leftEndToRightEnd > 0)
                {
                    var afterRightEnd = specificPoints.UnsafeNext(right.End);
                    // (DI) right during first
                    result = default(TIntervalUnions).NonEmpty(
                        default(TIntervals).Interval(left.Start, beforeRightStart),
                        default(TIntervals).Interval(afterRightEnd, left.End));
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (FI) right finishes left
                    result = default(TIntervalUnions).NonEmpty(
                        default(TIntervals).Interval(left.Start, beforeRightStart));
                }
                else
                {
                    // (O) left overlaps right
                    result = default(TIntervalUnions).NonEmpty(
                        default(TIntervals).Interval(left.Start, beforeRightStart));
                }
            }
        }

        public static void Union<TPoint, TInterval, TIntervalUnion, TIntervals, TIntervalUnions>(
            in TInterval left,
            in TInterval right,
            out TIntervalUnion result)
            where TPoint : IComparable<TPoint>
            where TInterval : IClosedInterval<TPoint>
            where TIntervals : struct, IClosedIntervals<TPoint, TInterval>
            where TIntervalUnions : struct, IIntervalUnions<TInterval, TIntervalUnion>
        {
            if (left.Start.CompareTo(right.End) > 0)
            {
                result = default(TIntervalUnions).NonEmpty(right, left);
                return;
            }
            if (left.End.CompareTo(right.Start) < 0)
            {
                result = default(TIntervalUnions).NonEmpty(left, right);
                return;
            }
            result = default(TIntervalUnions).NonEmpty(
                default(TIntervals).Interval(
                    left.Start.CompareTo(right.Start) < 0
                        ? left.Start
                        : right.Start,
                    left.End.CompareTo(right.End) > 0
                        ? left.End
                        : right.End));
        }
    }
}