// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges.Operations
{
    public static class OpenRangeOperations
    {
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