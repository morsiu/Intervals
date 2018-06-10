// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    public sealed class EqualsOperation
    {
        private readonly Range? _first;
        private readonly Range? _second;

        public EqualsOperation(Range? first, Range? second)
        {
            _first = first;
            _second = second;
        }

        public bool Result()
        {
            return new PointSequenceEqualityComparer().Equals(
                _first is Range x
                    ? new PointSequenceFromRange(x.Start, x.End,  x.HasOpenStart, x.HasOpenEnd)
                    : (IPointSequence)new EmptyPointSequence(),
                _second is Range y
                    ? new PointSequenceFromRange(y.Start, y.End, y.HasOpenStart, y.HasOpenEnd)
                    : (IPointSequence)new EmptyPointSequence());
        }
    }
}
