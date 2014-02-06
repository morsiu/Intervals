// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using Walrus.Ranges.Text;

namespace Walrus.Ranges.Test.Cases.Generation.Operations.StateMachines
{
    internal sealed class PointSequence
    {
        private readonly PointType[] _points;
        private readonly int _offset;

        public PointSequence()
        {
            _points = new PointType[0];
            _offset = 0;
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
            _offset = offset;
        }

        public IRange<int> ToRange()
        {
            var startIndex = Array.FindIndex(_points, point => point == PointType.ClosedEnd || point == PointType.OpenEnd);
            var endIndex = Array.FindLastIndex(_points, point => point == PointType.ClosedEnd || point == PointType.OpenEnd);

            if (startIndex == -1) return Range.Empty<int>();
            if (startIndex == endIndex && _points[startIndex] == PointType.OpenEnd) return Range.Empty<int>(); // HACK: Workaround for non-lookahead state machines generating degenerate ranges

            var start = _points[startIndex];
            var end = _points[endIndex];

            var builder = new RangeBuilder<int>();
            builder.SetStart(startIndex + _offset, start == PointType.OpenEnd);
            builder.SetEnd(endIndex + _offset, end == PointType.OpenEnd);
            return builder.Build();
        }

        public PointSequence Pad(PointSequence other)
        {
            var leftPadLength = _offset - other._offset;
            var rightPadLength = other._offset - _offset;
            var hasLeftPadding = leftPadLength > 0;
            var hasRightPadding = rightPadLength > 0;
            if (!hasLeftPadding && !hasRightPadding) return this;
            return new PointSequence(
                Pad(leftPadLength).Concat(_points).Concat(Pad(rightPadLength)),
                hasLeftPadding ? _offset - leftPadLength : _offset);
        }

        private IEnumerable<PointType> Pad(int length)
        {
            return length > 0
                ? Enumerable.Repeat(PointType.Uncovered, length)
                : Enumerable.Empty<PointType>();
        }

        public PointSequence Zip(PointSequence second, Func<PointType, PointType, PointType> zipper)
        {
            if (_offset != second._offset && _points.Length != second._points.Length) throw new ArgumentException("Second sequence must have identical offset and number count.", "second");
            return new PointSequence(
                _points.Zip(second._points, zipper),
                _offset);
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
    }
}