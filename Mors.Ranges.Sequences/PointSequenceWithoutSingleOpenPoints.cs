// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections;
using System.Collections.Generic;

namespace Mors.Ranges.Sequences
{
    public sealed class PointSequenceWithoutSingleOpenPoints : IPointSequence
    {
        private readonly IPointSequence _sequence;

        public PointSequenceWithoutSingleOpenPoints(IPointSequence sequence)
        {
            _sequence = sequence;
        }

        public int Start => _sequence.Start;

        public int Length => _sequence.Length;

        public IEnumerator<PointType> GetEnumerator()
        {
            var point = _sequence.GetEnumerator();
            if (!point.MoveNext()) yield break;
            var previous = point.Current;
            if (!point.MoveNext())
            {
                yield return
                    previous == PointType.OpenEnd
                        ? PointType.Uncovered
                        : previous;
                yield break;
            }
            var current = point.Current;
            yield return
                previous == PointType.OpenEnd && current == PointType.Uncovered
                    ? PointType.Uncovered
                    : previous;
            if (!point.MoveNext())
            {
                yield return
                    previous == PointType.Uncovered && current == PointType.OpenEnd
                        ? PointType.Uncovered
                        : current;
                yield break;
            }
            var next = point.Current;
            do
            {
                yield return
                    previous == PointType.Uncovered && current == PointType.OpenEnd && next == PointType.Uncovered
                        ? PointType.Uncovered
                        : current;
                if (!point.MoveNext())
                {
                    yield return
                        current == PointType.Uncovered && next == PointType.OpenEnd
                            ? PointType.Uncovered
                            : next;
                    yield break;
                }
                else
                {
                    previous = current;
                    current = next;
                    next = point.Current;
                }
            }
            while (true);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}