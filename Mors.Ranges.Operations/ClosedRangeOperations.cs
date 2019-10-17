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
    }
}