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

        public override string ToString() =>
            $"{Start}: {string.Concat(this.Select(PointTypeCharacters.Character))}";
    }
}
