// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges.Operations
{
    public class ClosedRangeOperations
    {
        public static bool Covers<T, TRange>(
            in TRange left,
            in TRange right)
            where T : IComparable<T>
            where TRange : IClosedRange<T>
        {
            if (!IntersectsWith<T, TRange>(left, right))
                return false;

            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);

            return (leftStartToRightStart < 0 || leftStartToRightStart == 0)
                   && (leftEndToRightEnd > 0 || leftEndToRightEnd  == 0);
        }
        
        public static bool IntersectsWith<T, TRange>(
            in TRange left,
            in TRange right)
            where T : IComparable<T>
            where TRange : IClosedRange<T>
        {
            if (left.Empty || right.Empty) return false;
            if (left.Start.CompareTo(right.End) > 0) return false;
            if (left.End.CompareTo(right.Start) < 0) return false;
            return true;
        }
        
        public static void Intersect<T, TRange, TRanges>(
            in TRange left,
            in TRange right,
            out TRange result)
            where T : IComparable<T>
            where TRange : IClosedRange<T>
            where TRanges : struct, IClosedRanges<T, TRange>
        {
            if (!IntersectsWith<T, TRange>(left, right))
            {
                result = default(TRanges).Empty();
                return;
            }
            var leftStartToRightStart = left.Start.CompareTo(right.Start);
            var leftEndToRightEnd = left.End.CompareTo(right.End);
            result = default(TRanges).NonEmpty(
                leftStartToRightStart > 0 ? left.Start : right.Start,
                leftEndToRightEnd < 0 ? left.End : right.End);
        }
        
        public static bool IsCoveredBy<T, TRange>(
            in TRange left,
            in TRange right)
            where T : IComparable<T>
            where TRange : IClosedRange<T>
        {
            return Covers<T, TRange>(right, left);
        }
        
        public static void Span<T, TRange, TRanges>(
            in TRange left,
            in TRange right,
            out TRange result)
            where T : IComparable<T>
            where TRange : IClosedRange<T>
            where TRanges : struct, IClosedRanges<T, TRange>
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

            result = default(TRanges).NonEmpty(
                leftStartToRightStart < 0 ? left.Start : right.Start,
                leftEndToRightEnd > 0 ? left.End : right.End);
        }
    }
}