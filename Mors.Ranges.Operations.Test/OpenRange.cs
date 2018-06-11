// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations
{
    public readonly struct OpenRange : IRange<int>, IOpenRange, IEmptyRange, IEquatable<OpenRange>
    {
        private readonly bool _nonEmpty;

        public OpenRange(int start, int end, bool openStart, bool openEnd)
            : this(
                true,
                start,
                end,
                openStart,
                openEnd)
        {
        }

        private OpenRange(bool nonEmpty, int start, int end, bool openStart, bool openEnd)
        {
            Start = start;
            End = end;
            OpenStart = openStart;
            OpenEnd = openEnd;
            _nonEmpty = nonEmpty;
        }
        
        public int Start { get; }
        public int End { get; }
        public bool Empty => !_nonEmpty;
        public bool OpenStart { get; }
        public bool OpenEnd { get; }

        public bool Equals(OpenRange other) =>
            other._nonEmpty == _nonEmpty 
            && other.OpenStart == OpenStart 
            && other.OpenEnd == OpenEnd
            && other.Start == Start
            && End == other.End;

        public override bool Equals(object obj) => obj is OpenRange other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _nonEmpty.GetHashCode();
                hashCode = (hashCode * 397) ^ Start;
                hashCode = (hashCode * 397) ^ End;
                hashCode = (hashCode * 397) ^ OpenStart.GetHashCode();
                hashCode = (hashCode * 397) ^ OpenEnd.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() =>
            _nonEmpty 
                ? $"{(OpenStart ? "(" : "[")}{Start}, {End}{(OpenEnd ? ")" : "]")}"
                : "{empty}";

        public Range? Range() =>
            _nonEmpty
                ? new Range(Start, End, OpenStart, OpenEnd)
                : default(Range?);
        
        public IPointSequence PointSequence() =>
            _nonEmpty
                ? new PointSequenceFromRange(Start, End, OpenStart, OpenEnd)
                : (IPointSequence) new EmptyPointSequence();
    }
}
