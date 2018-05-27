// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Sequences
{
    public sealed class PointSequenceFromRange : IPointSequence
    {
        private readonly int _start;
        private readonly int _end;
        private readonly bool _hasOpenStart;
        private readonly bool _hasOpenEnd;

        public PointSequenceFromRange(int start, int end, bool hasOpenStart, bool hasOpenEnd)
        {
            if (start > end) throw new ArgumentException("Parameter end has to be greater than or equal to the start parameter.");
            _start = start;
            _end = end;
            _hasOpenStart = hasOpenStart;
            _hasOpenEnd = hasOpenEnd;
        }

        public int Start => _start;

        public int Length => _end - _start + 1;

        public IEnumerator<PointType> GetEnumerator()
        {
            return
                _start == _end
                    ? OnePointRange()
                    : MultiplePointsRange();
            
            IEnumerator<PointType> OnePointRange()
            {
                yield return
                    _hasOpenStart || _hasOpenEnd
                        ? PointType.Uncovered
                        : PointType.ClosedEnd;
            }

            IEnumerator<PointType> MultiplePointsRange()
            {
                yield return _hasOpenStart ? PointType.OpenEnd : PointType.ClosedEnd;
                for (int point = _start + 1; point < _end; point++)
                {
                    yield return PointType.Covered;
                }
                yield return _hasOpenEnd ? PointType.OpenEnd : PointType.ClosedEnd;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}