using System;

namespace Mors.Ranges.Operations
{
    public readonly struct ClosedRange
        : IClosedRange<int>,
        Reference.IClosedRange<int>,
        IEmptyRange,
        Reference.IEmptyRange,
        IEquatable<ClosedRange>
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
    }
}
