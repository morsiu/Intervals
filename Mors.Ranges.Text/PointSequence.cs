﻿// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Text
{
    public sealed class PointSequence : IEnumerable<PointType>
    {
        private readonly PointType[] _points;

        public PointSequence()
        {
            _points = new PointType[0];
            Offset = 0;
        }

        public static PointSequence FromRange(IRange<int> range)
        {
            if (range.IsEmpty)
            {
                return new PointSequence();
            }

            var points = Enumerate(range);
            return new PointSequence(points, range.Start);
        }

        public PointSequence(IEnumerable<PointType> points, int offset)
        {
            _points = points.ToArray();
            Offset = offset;
        }

        public IRange<int> ToRange()
        {
            var startIndex = Array.FindIndex(_points, point => point == PointType.ClosedEnd || point == PointType.OpenEnd);
            var endIndex = Array.FindLastIndex(_points, point => point == PointType.ClosedEnd || point == PointType.OpenEnd);

            if (startIndex == -1) return Range.Empty<int>();
            if (startIndex == endIndex && _points[startIndex] == PointType.OpenEnd) return Range.Empty<int>(); // HACK: Workaround for non-lookahead state machines generating degenerate ranges (like '--o---')

            var start = _points[startIndex];
            var end = _points[endIndex];

            var builder = new RangeBuilder<int>();
            builder.SetStart(startIndex + Offset, start == PointType.OpenEnd);
            builder.SetEnd(endIndex + Offset, end == PointType.OpenEnd);
            return builder.Build();
        }

        public PointSequence Pad(PointSequence other)
        {
            var leftPadLength = Math.Max(0, Offset - other.Offset);
            var rightPadLength = Math.Max(0, other.Offset + other._points.Length - Offset - _points.Length);
            var hasLeftPadding = leftPadLength > 0;
            var hasRightPadding = rightPadLength > 0;
            if (!hasLeftPadding && !hasRightPadding) return this;
            return new PointSequence(
                Pad(leftPadLength).Concat(_points).Concat(Pad(rightPadLength)),
                hasLeftPadding ? Offset - leftPadLength : Offset);
        }

        private static IEnumerable<PointType> Enumerate(IRange<int> range)
        {
            yield return range.HasOpenStart
                ? PointType.OpenEnd
                : PointType.ClosedEnd;

            for (var idx = range.Start + 1; idx < range.End; ++idx)
                yield return PointType.Covered;

            if (range.End != range.Start)
                yield return range.HasOpenEnd ? PointType.OpenEnd : PointType.ClosedEnd;
        }

        private static IEnumerable<PointType> Pad(int length)
        {
            return Enumerable.Repeat(PointType.Uncovered, length);
        }

        public IEnumerator<PointType> GetEnumerator()
        {
            foreach (var point in _points) yield return point;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Offset { get; }
    }
}