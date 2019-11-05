using System;

namespace Mors.Ranges.Operations
{
    public static class ClosedRangeOperations
    {
        public static bool Contains<TPoint, TRange>(
            in TRange range,
            in TPoint point)
            where TPoint : IComparable<TPoint>
            where TRange : IClosedRange<TPoint>, IEmptyRange
        {
            return !range.Empty
                   && point.CompareTo(range.Start) >= 0
                   && point.CompareTo(range.End) <= 0;
        }
        
        public static bool Covers<TPoint, TRange>(
            in TRange left,
            in TRange right)
            where TPoint : IComparable<TPoint>
            where TRange : IClosedRange<TPoint>, IEmptyRange
        {
            if (!IntersectsWith<TPoint, TRange>(left, right))
                return false;

            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);

            return (leftStartToRightStart < 0 || leftStartToRightStart == 0)
                   && (leftEndToRightEnd > 0 || leftEndToRightEnd  == 0);
        }
        
        public static bool IntersectsWith<TPoint, TRange>(
            in TRange left,
            in TRange right)
            where TPoint : IComparable<TPoint>
            where TRange : IClosedRange<TPoint>, IEmptyRange
        {
            if (left.Empty || right.Empty) return false;
            if (left.Start.CompareTo(right.End) > 0) return false;
            if (left.End.CompareTo(right.Start) < 0) return false;
            return true;
        }
        
        public static void Intersect<TPoint, TRange, TRanges>(
            in TRange left,
            in TRange right,
            out TRange result)
            where TPoint : IComparable<TPoint>
            where TRange : IClosedRange<TPoint>, IEmptyRange
            where TRanges : struct, IClosedRanges<TPoint, TRange>, IEmptyRanges<TRange>
        {
            if (!IntersectsWith<TPoint, TRange>(left, right))
            {
                result = default(TRanges).Empty();
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            result = default(TRanges).Range(
                leftStartToRightStart > 0 ? left.Start : right.Start,
                leftEndToRightEnd < 0 ? left.End : right.End);
        }
        
        public static bool IsCoveredBy<TPoint, TRange>(
            in TRange left,
            in TRange right)
            where TPoint : IComparable<TPoint>
            where TRange : IClosedRange<TPoint>, IEmptyRange
        {
            return Covers<TPoint, TRange>(right, left);
        }
        
        public static void Span<TPoint, TRange, TRanges>(
            in TRange left,
            in TRange right,
            out TRange result)
            where TPoint : IComparable<TPoint>
            where TRange : IClosedRange<TPoint>, IEmptyRange
            where TRanges : struct, IClosedRanges<TPoint, TRange>
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
            result = default(TRanges).Range(
                leftStartToRightStart < 0 ? left.Start : right.Start,
                leftEndToRightEnd > 0 ? left.End : right.End);
        }

        public static void Subtract<TPoint, TRange, TRangeUnion, TRanges, TRangeUnions>(
            in TRange left,
            in TRange right,
            IPoints points,
            out TRangeUnion result)
            where TPoint : IComparable<TPoint>
            where TRange : IClosedRange<TPoint>, IEmptyRange
            where TRanges : struct, IClosedRanges<TPoint, TRange>
            where TRangeUnions : struct, IRangeUnions<TRange, TRangeUnion>
        {
            if (left.Empty)
            {
                result = default(TRangeUnions).Empty();
                return;
            }
            if (right.Empty)
            {
                result = default(TRangeUnions).NonEmpty(left);
                return;
            }
            var leftStartToRightEnd = left.Start.CompareTo(right.End);
            if (leftStartToRightEnd > 0)
            {
                // (BI) right before left
                result = default(TRangeUnions).NonEmpty(left);
                return;
            }
            if (leftStartToRightEnd == 0)
            {
                // (MI) right meets left
                result = default(TRangeUnions).NonEmpty(
                    default(TRanges).Range(points.For<TPoint>().UnsafeNext(right.End), left.End));
                return;
            }
            var leftEndToRightStart = left.End.CompareTo(right.Start);
            if (leftEndToRightStart == 0)
            {
                // (M) left meets right
                result = default(TRangeUnions).NonEmpty(
                    default(TRanges).Range(left.Start, points.For<TPoint>().UnsafePrevious(right.Start)));
                return;
            }
            if (leftEndToRightStart < 0)
            {
                // (B) left before right
                result = default(TRangeUnions).NonEmpty(left);
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            if (leftStartToRightStart > 0)
            {
                var leftEndToRightEnd = left.End.CompareTo(right.End);
                if (leftEndToRightEnd < 0)
                {
                    // (D) left during right
                    result = default(TRangeUnions).Empty();
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (F) left finishes right
                    result = default(TRangeUnions).Empty();
                }
                else
                {
                    // (OI) right overlaps left
                    result = default(TRangeUnions).NonEmpty(
                        default(TRanges).Range(points.For<TPoint>().UnsafeNext(right.End), left.End));
                }
            }
            else if (leftStartToRightStart == 0)
            {
                var leftEndToRightEnd = left.End.CompareTo(right.End);
                if (leftEndToRightEnd > 0)
                {
                    // (SI) right starts left
                    result = default(TRangeUnions).NonEmpty(
                        default(TRanges).Range(points.For<TPoint>().UnsafeNext(right.End), left.End));
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (EQ) left equals right
                    result = default(TRangeUnions).Empty();
                }
                else
                {
                    // (S) left starts right
                    result = default(TRangeUnions).Empty();
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
                    result = default(TRangeUnions).NonEmpty(
                        default(TRanges).Range(left.Start, beforeRightStart),
                        default(TRanges).Range(afterRightEnd, left.End));
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (FI) right finishes left
                    result = default(TRangeUnions).NonEmpty(
                        default(TRanges).Range(left.Start, beforeRightStart));
                }
                else
                {
                    // (O) left overlaps right
                    result = default(TRangeUnions).NonEmpty(
                        default(TRanges).Range(left.Start, beforeRightStart));
                }
            }
        }

        public static void Union<TPoint, TRange, TRangeUnion, TRanges, TRangeUnions>(
            in TRange left,
            in TRange right,
            out TRangeUnion result)
            where TPoint : IComparable<TPoint>
            where TRange : IClosedRange<TPoint>, IEmptyRange
            where TRanges : struct, IClosedRanges<TPoint, TRange>
            where TRangeUnions : struct, IRangeUnions<TRange, TRangeUnion>
        {
            if (left.Empty && right.Empty)
            {
                result = default(TRangeUnions).Empty();
                return;
            }
            if (left.Empty)
            {
                result = default(TRangeUnions).NonEmpty(right);
                return;
            }
            if (right.Empty)
            {
                result = default(TRangeUnions).NonEmpty(left);
                return;
            }
            if (left.Start.CompareTo(right.End) > 0)
            {
                result = default(TRangeUnions).NonEmpty(right, left);
                return;
            }
            if (left.End.CompareTo(right.Start) < 0)
            {
                result = default(TRangeUnions).NonEmpty(left, right);
                return;
            }
            result = default(TRangeUnions).NonEmpty(
                default(TRanges).Range(
                    left.Start.CompareTo(right.Start) < 0
                        ? left.Start
                        : right.Start,
                    left.End.CompareTo(right.End) > 0
                        ? left.End
                        : right.End));
        }
    }
}