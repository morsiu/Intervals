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
