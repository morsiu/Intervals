using System;

namespace Mors.Ranges.Operations
{
    public static class OpenRangeOperations
    {
        public static bool Contains<TPoint, TRange>(
            in TRange range,
            in TPoint point)
            where TPoint : IComparable<TPoint>
            where TRange : IOpenRange<TPoint>, IEmptyRange
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
            where TRange : IOpenRange<TPoint>, IEmptyRange
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
            where TRange : IOpenRange<TPoint>, IEmptyRange
        {
            return Covers<TPoint, TRange>(right, left);
        }
        
        public static void Intersect<TPoint, TRange, TRanges>(
            in TRange left,
            in TRange right,
            out TRange result)
            where TPoint : IComparable<TPoint>
            where TRange : IOpenRange<TPoint>, IEmptyRange
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
            where TRange : IOpenRange<TPoint>, IEmptyRange
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
            where TRange : IOpenRange<TPoint>, IEmptyRange
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

        public static void Subtract<TPoint, TRange, TRangeUnion, TRanges, TRangeUnions>(
            in TRange left,
            in TRange right,
            out TRangeUnion result)
            where TPoint : IComparable<TPoint>
            where TRange : IOpenRange<TPoint>, IEmptyRange
            where TRanges : struct, IOpenRanges<TPoint, TRange>
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
                    default(TRanges).Range(left.Start, left.End, left.OpenStart || !right.OpenEnd, left.OpenEnd));
                return;
            }
            var leftEndToRightStart = left.End.CompareTo(right.Start);
            if (leftEndToRightStart == 0)
            {
                // (M) left meets right
                result = default(TRangeUnions).NonEmpty(
                    default(TRanges).Range(left.Start, left.End, left.OpenStart, left.OpenEnd || !right.OpenStart));
                return;
            }
            if (leftEndToRightStart < 0)
            {
                // (B) left before right
                result = default(TRangeUnions).NonEmpty(left);
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            if (leftStartToRightStart > 0)
            {
                if (leftEndToRightEnd > 0)
                {
                    // (OI) right overlaps left
                    result = default(TRangeUnions).NonEmpty(
                        default(TRanges).Range(right.End, left.End, !right.OpenEnd, left.OpenEnd));
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (F) left finishes right
                    result = !left.OpenEnd && right.OpenEnd
                        ? default(TRangeUnions).NonEmpty(
                            default(TRanges).Range(left.End, left.End, openStart: false, openEnd: false))
                        : default(TRangeUnions).Empty();
                }
                else
                {
                    // (D) left during right
                    result = default(TRangeUnions).Empty();
                }
            }
            else if (leftStartToRightStart == 0)
            {
                if (leftEndToRightEnd > 0)
                {
                    // (SI) right starts left
                    var second = default(TRanges).Range(right.End, left.End, !right.OpenEnd, left.OpenEnd);
                    result = !left.OpenStart && right.OpenStart
                        ? default(TRangeUnions).NonEmpty(
                            default(TRanges).Range(left.Start, left.Start, openStart: false, openEnd: false),
                            second)
                        : default(TRangeUnions).NonEmpty(second);
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (EQ) left equals right
                    var startIncluded = !left.OpenStart && right.OpenStart;
                    var endIncluded = !left.OpenEnd && right.OpenEnd;
                    result = (startIncluded, endIncluded) switch
                    {
                        (true, true) => default(TRangeUnions).NonEmpty(
                            default(TRanges).Range(left.Start, left.Start, openStart: false, openEnd: false),
                            default(TRanges).Range(left.End, left.End, openStart: false, openEnd: false)),
                        (true, false) => default(TRangeUnions).NonEmpty(
                            default(TRanges).Range(left.Start, left.Start, openStart: false, openEnd: false)),
                        (false, true) => default(TRangeUnions).NonEmpty(
                            default(TRanges).Range(left.End, left.End, openStart: false, openEnd: false)),
                        _ => default(TRangeUnions).Empty()
                    };
                }
                else
                {
                    // (S) left starts right
                    result = !left.OpenStart && right.OpenStart
                        ? default(TRangeUnions).NonEmpty(
                            default(TRanges).Range(left.Start, left.Start, openStart: false, openEnd: false))
                        : default(TRangeUnions).Empty();
                }
            }
            else
            {
                if (leftEndToRightEnd > 0)
                {
                    // (DI) right during first
                    result = default(TRangeUnions).NonEmpty(
                        default(TRanges).Range(left.Start, right.Start, left.OpenStart, !right.OpenStart),
                        default(TRanges).Range(right.End, left.End, !right.OpenEnd, left.OpenEnd));
                }
                else if (leftEndToRightEnd == 0)
                {
                    // (FI) right finishes left
                    var first = default(TRanges).Range(left.Start, right.Start, left.OpenStart, !right.OpenStart);
                    result = !left.OpenEnd && right.OpenEnd
                        ? default(TRangeUnions).NonEmpty(first,
                            default(TRanges).Range(left.End, left.End, openStart: false, openEnd: false))
                        : default(TRangeUnions).NonEmpty(first);
                }
                else
                {
                    // (O) left overlaps right
                    result = default(TRangeUnions).NonEmpty(
                        default(TRanges).Range(left.Start, right.Start, left.OpenStart, !right.OpenStart));
                }
            }
        }

        public static void Union<TPoint, TRange, TRangeUnion, TRanges, TRangeUnions>(
            in TRange left,
            in TRange right,
            out TRangeUnion result)
            where TPoint : IComparable<TPoint>
            where TRange : IOpenRange<TPoint>, IEmptyRange
            where TRanges : struct, IOpenRanges<TPoint, TRange>
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
            var leftStartToRightEnd = left.Start.CompareTo(right.End);
            if (leftStartToRightEnd > 0
                || (leftStartToRightEnd == 0 && left.OpenStart && right.OpenEnd))
            {
                result = default(TRangeUnions).NonEmpty(right, left);
                return;
            }
            var leftEndToRightStart = left.End.CompareTo(right.Start);
            if (leftEndToRightStart < 0
                || (leftEndToRightStart == 0 && left.OpenEnd && right.OpenStart))
            {
                result = default(TRangeUnions).NonEmpty(left, right);
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            result = default(TRangeUnions).NonEmpty(
                default(TRanges).Range(
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