// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations
{
    public readonly struct ClosedRange : IClosedRange<int>, IRange<int>, IEquatable<ClosedRange>
    {
        private readonly IRange<int> _range;

        public ClosedRange(IPointSequence pointSequence)
            : this(new FirstRangeInPointSequence(pointSequence))
        {
        }

        public ClosedRange(Sequences.Range? range)
            : this(
                range != null 
                    ? Ranges.Range.Create(
                        range.Value.Start,
                        range.Value.End,
                        range.Value.HasOpenStart,
                        range.Value.HasOpenEnd)
                    : Ranges.Range.Empty<int>())
        {
        }

        public ClosedRange(IRange<int> range)
        {
            if (!range.IsEmpty && (range.HasOpenEnd || range.HasOpenStart))
            {
                throw new ArgumentException("Range has open ends.", nameof(range));
            }
            _range = range;
        }

        public bool IsEmpty => _range.IsEmpty;

        public int Start => _range.Start;

        public int End => _range.End;

        public bool HasOpenStart => _range.HasOpenStart;

        public bool HasOpenEnd => _range.HasOpenEnd;

        public bool Empty => _range.IsEmpty;

        public bool Equals(IRange<int> other) => _range.Equals(other);

        public bool Equals(ClosedRange other) => _range.Equals(other._range);

        public override bool Equals(object obj) => obj is ClosedRange other && Equals(other);

        public override int GetHashCode() => _range.GetHashCode();

        public override string ToString() => _range.ToString();

        public Sequences.Range? Range() =>
            _range.IsEmpty
                ? default(Sequences.Range?)
                : new Sequences.Range(_range.Start, _range.End, _range.HasOpenStart, _range.HasOpenEnd);
        
        public IPointSequence PointSequence() =>
            _range.IsEmpty
                ? (IPointSequence) new EmptyPointSequence()
                : new PointSequenceFromRange(_range.Start, _range.End, _range.HasOpenStart, _range.HasOpenEnd);
    }
}
