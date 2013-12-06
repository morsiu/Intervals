// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Walrus.Ranges
{
    public static class Range
    {
        public static IRange<T> Point<T>(T point)
            where T : IComparable<T>
        {
            return new Range<T>(point, point, false, false);
        }

        public static IRange<T> Closed<T>(T start, T end)
            where T : IComparable<T>
        {
            RangeValidator.CheckStartNotGreaterThanEnd(start, end);
            return new Range<T>(start, end, false, false);
        }

        public static IRange<T> Open<T>(T start, T end)
            where T : IComparable<T>
        {
            RangeValidator.CheckEndGreaterThanStart(start, end);
            return new Range<T>(start, end, true, true);
        }

        public static IRange<T> LeftOpen<T>(T start, T end)
            where T : IComparable<T>
        {
            RangeValidator.CheckEndGreaterThanStart(start, end);
            return new Range<T>(start, end, true, false);
        }

        public static IRange<T> RightOpen<T>(T start, T end)
            where T : IComparable<T>
        {
            RangeValidator.CheckEndGreaterThanStart(start, end);
            return new Range<T>(start, end, false, true);
        }

        public static IRange<T> LeftClosed<T>(T start, T end)
            where T : IComparable<T>
        {
            return RightOpen(start, end);
        }

        public static IRange<T> RightClosed<T>(T start, T end)
            where T : IComparable<T>
        {
            return LeftOpen(start, end);
        }

        public static IRange<T> Empty<T>()
            where T : IComparable<T>
        {
            return EmptyRange<T>.Value;
        }

        public static IRange<T> Create<T>(T start, T end, bool hasOpenStart, bool hasOpenEnd)
            where T : IComparable<T>
        {
            if (!hasOpenStart && !hasOpenEnd) return Closed(start, end);
            if (!hasOpenStart && hasOpenEnd) return RightOpen(start, end);
            if (hasOpenStart && !hasOpenEnd) return LeftOpen(start, end);
            return Open(start, end);
        }
    }
}
