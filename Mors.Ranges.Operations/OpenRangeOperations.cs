using System;

namespace Mors.Ranges.Operations
{
    public static class OpenRangeOperations
    {
        public static bool Contains<TPoint, TRange>(
            in TRange range,
            in TPoint point)
            where TPoint : IComparable<TPoint>
            where TRange : IRange<TPoint>, IEmptyRange, IOpenRange
        {
            if (range.Empty) return false;
            var pointToStart = point.CompareTo(range.Start);
            var pointToEnd = point.CompareTo(range.End);
            if (pointToStart < 0) return false;
            if (pointToStart == 0) return pointToEnd != 0 ? !range.OpenStart : !range.OpenStart && !range.OpenEnd;
            if (pointToEnd < 0) return true;
            if (pointToEnd == 0) return !range.OpenEnd;
            return false;
        }

        public static bool Covers<TPoint, TRange>(
            in TRange left,
            in TRange right)
            where TPoint : IComparable<TPoint>
            where TRange : IRange<TPoint>, IEmptyRange, IOpenRange
        {
            if (!IntersectsWith<TPoint, TRange>(left, right))
                return false;

            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);

            return (leftStartToRightStart < 0 || (leftStartToRightStart == 0 && (!left.OpenStart || right.OpenStart)))
                && (leftEndToRightEnd > 0 || (leftEndToRightEnd  == 0 && (!left.OpenEnd || right.OpenEnd)));
        }
        
        public static bool IsCoveredBy<TPoint, TRange>(
            in TRange left,
            in TRange right)
            where TPoint : IComparable<TPoint>
            where TRange : IRange<TPoint>, IEmptyRange, IOpenRange
        {
            return Covers<TPoint, TRange>(right, left);
        }
        
        public static void Intersect<TPoint, TRange, TRanges>(
            in TRange left,
            in TRange right,
            out TRange result)
            where TPoint : IComparable<TPoint>
            where TRange : IRange<TPoint>, IEmptyRange, IOpenRange
            where TRanges : struct, IOpenRanges<TPoint, TRange>, IEmptyRanges<TRange>
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
        
        public static bool IntersectsWith<TPoint, TRange>(
            in TRange left,
            in TRange right)
            where TPoint : IComparable<TPoint>
            where TRange : IRange<TPoint>, IEmptyRange, IOpenRange
        {
            if (left.Empty || right.Empty) return false;
            
            var leftStartToRightEnd = left.Start.CompareTo(right.End);
            
            if (leftStartToRightEnd > 0) return false;
            
            var leftEndToRightStart = left.End.CompareTo(right.Start);
            
            if (leftEndToRightStart < 0) return false;
            if (leftStartToRightEnd == 0) return !left.OpenStart && !right.OpenEnd;
            if (leftEndToRightStart == 0) return !left.OpenEnd && !right.OpenStart;
            return true;
        }
        
        public static void Span<TPoint, TRange, TRanges>(
            in TRange left,
            in TRange right,
            out TRange result)
            where TPoint : IComparable<TPoint>
            where TRange : IRange<TPoint>, IEmptyRange, IOpenRange
            where TRanges : struct, IOpenRanges<TPoint, TRange>
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
    }
}