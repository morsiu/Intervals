// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations
{
    public readonly struct ClosedRange : IRange<int>, IEmptyRange, IEquatable<ClosedRange>
    {
        private readonly bool _nonEmpty;

        public ClosedRange(int start, int end)
            : this(true, start, end)
        {
        }

        private ClosedRange(bool nonEmpty, int start, int end)
        {
            _nonEmpty = nonEmpty;
            Start = start;
            End = end;
        }

        public bool Empty => !_nonEmpty;

        public int Start { get; }

        public int End { get; }

        public bool Equals(ClosedRange other) =>
            other._nonEmpty == _nonEmpty
            && other.Start == Start
            && other.End == End;

        public override bool Equals(object obj) => obj is ClosedRange other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _nonEmpty.GetHashCode();
                hashCode = (hashCode * 397) ^ Start;
                hashCode = (hashCode * 397) ^ End;
                return hashCode;
            }
        }

        public override string ToString() =>
            _nonEmpty
                ? $"[{Start}, {End}]"
                : "{empty}";

        public IPointSequence PointSequence() =>
            _nonEmpty
                ? new PointSequenceFromRange(Start, End, false, false)
                : (IPointSequence) new EmptyPointSequence();
    }
}
