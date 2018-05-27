// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Sequences.Tests
{
    public sealed class TestablePointSequence : IPointSequence, IEquatable<TestablePointSequence>
    {
        private readonly IPointSequence _sequence;

        public TestablePointSequence(string text, int start)
            : this(new PointSequenceFromString(text, start))
        {
        }

        public TestablePointSequence(IPointSequence sequence)
        {
            _sequence = sequence;
        }

        public int Start => _sequence.Start;

        public int Length => _sequence.Length;

        public IEnumerator<PointType> GetEnumerator() => _sequence.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_sequence).GetEnumerator();

        public bool Equals(TestablePointSequence other) => new PointSequenceEqualityComparer().Equals(this, other);

        public override bool Equals(object obj) =>
            obj is TestablePointSequence other && Equals(other);

        public override int GetHashCode() =>
            new PointSequenceEqualityComparer().GetHashCode(this);

        public override string ToString()
        {
            return $"{Start}: {string.Concat(this.Select(PointTypeCharacter))}";

            char PointTypeCharacter(PointType pointType)
            {
                switch (pointType)
                {
                    case PointType.ClosedEnd: return 'x';
                    case PointType.Covered: return '=';
                    case PointType.OpenEnd: return 'o';
                    case PointType.Uncovered: return '-';
                    default: throw new ArgumentException($"The point type {pointType} is not supported.", nameof(pointType));
                }
            }
        }
    }
}
