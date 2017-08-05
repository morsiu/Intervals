// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges
{
    public static class RangeOperations
    {
        public static bool IntersectsWith<T>(IRange<T> x, IRange<T> y)
            where T : IComparable<T>
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));
            if (x.IsEmpty || y.IsEmpty) return false;
            if (x.Start.CompareTo(y.End) > 0) return false;
            if (x.End.CompareTo(y.Start) < 0) return false;
            if (x.Start.CompareTo(y.End) == 0) return !x.HasOpenStart && !y.HasOpenEnd;
            if (x.End.CompareTo(y.Start) == 0) return !x.HasOpenEnd && !y.HasOpenStart;
            return true;
        }

        public static IRange<T> Intersect<T>(IRange<T> x, IRange<T> y)
            where T : IComparable<T>
        {
            if (!IntersectsWith(x, y)) return Range.Empty<T>();

            var xStartToYStart = x.Start.CompareTo(y.Start);
            var xEndToYEnd = x.End.CompareTo(y.End);

            return Range.Create(
                xStartToYStart > 0 ? x.Start : y.Start,
                xEndToYEnd < 0 ? x.End : y.End,
                xStartToYStart == 0
                    ? x.HasOpenStart || y.HasOpenStart
                    : xStartToYStart > 0
                        ? x.HasOpenStart
                        : y.HasOpenStart,
                xEndToYEnd == 0
                    ? x.HasOpenEnd || y.HasOpenEnd
                    : xEndToYEnd < 0
                        ? x.HasOpenEnd
                        : y.HasOpenEnd);
        }

        public static bool Covers<T>(IRange<T> x, IRange<T> y)
            where T : IComparable<T>
        {
            if (!IntersectsWith(x, y)) return false;

            var xStartToYStart = x.Start.CompareTo(y.Start);
            var xEndToYEnd = x.End.CompareTo(y.End);

            return (xStartToYStart < 0 || (xStartToYStart == 0 && (!x.HasOpenStart || (x.HasOpenStart && y.HasOpenStart))))
                && (xEndToYEnd > 0 || (xEndToYEnd == 0 && (!x.HasOpenEnd || (x.HasOpenEnd && y.HasOpenEnd))));
        }

        public static bool IsCoveredBy<T>(IRange<T> x, IRange<T> y)
            where T : IComparable<T>
        {
            return Covers(y, x);
        }

        public static IRange<T> Span<T>(IRange<T> x, IRange<T> y)
            where T : IComparable<T>
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));
            if (x.IsEmpty) return y;
            if (y.IsEmpty) return x;

            var xStartToYStart = x.Start.CompareTo(y.Start);
            var xEndToYEnd = x.End.CompareTo(y.End);

            return Range.Create(
                xStartToYStart < 0 ? x.Start : y.Start,
                xEndToYEnd > 0 ? x.End : y.End,
                xStartToYStart == 0 
                    ? x.HasOpenStart && y.HasOpenStart
                    : xStartToYStart < 0 ? x.HasOpenStart : y.HasOpenStart,
                xEndToYEnd == 0
                    ? x.HasOpenEnd && y.HasOpenEnd
                    : xEndToYEnd > 0 ? x.HasOpenEnd : y.HasOpenEnd);
        }
    }
}
