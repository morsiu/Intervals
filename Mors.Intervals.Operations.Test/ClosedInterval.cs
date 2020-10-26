using System;

namespace Mors.Intervals.Operations.Test
{
    public readonly struct ClosedInterval
        : IClosedInterval<int>,
        Reference.IClosedInterval<int>,
        IEmptyInterval,
        Reference.IEmptyInterval,
        IEquatable<ClosedInterval>
    {
        private readonly bool _nonEmpty;

        public ClosedInterval(int start, int end)
            : this(true, start, end)
        {
        }

        private ClosedInterval(bool nonEmpty, int start, int end)
        {
            _nonEmpty = nonEmpty;
            Start = start;
            End = end;
        }

        public bool Empty => !_nonEmpty;

        public int Start { get; }

        public int End { get; }

        public bool Equals(ClosedInterval other) =>
            other._nonEmpty == _nonEmpty
            && other.Start == Start
            && other.End == End;

        public override bool Equals(object? obj) => obj is ClosedInterval other && Equals(other);

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
