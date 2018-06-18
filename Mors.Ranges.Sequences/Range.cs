// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges.Sequences
{
    public readonly struct Range : IEquatable<Range>
    {
        public Range(int start, int end, bool hasOpenStart, bool hasOpenEnd)
        {
            Start = start;
            End = end;
            HasOpenStart = hasOpenStart;
            HasOpenEnd = hasOpenEnd;
        }

        public int Start { get; }
        public int End { get; }
        public bool HasOpenStart { get; }
        public bool HasOpenEnd { get; }

        public bool Equals(Range other) =>
            other.Start == Start
            && other.End == End
            && other.HasOpenStart == HasOpenStart
            && other.HasOpenEnd == HasOpenEnd;

        public override bool Equals(object obj) =>
            obj is Range other && Equals(other);

        public override int GetHashCode() =>
            (((((Start * 397) ^ End) * 397) ^ HasOpenStart.GetHashCode()) * 397) ^ HasOpenEnd.GetHashCode();

        public override string ToString() =>
            string.Concat(HasOpenStart ? '(' : '[', Start, ", ", End, HasOpenEnd ? ')' : ']');
    }
}
