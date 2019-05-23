using System;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations
{
    public readonly struct OpenRange
        : IRange<int>,
        Reference.IRange<int>,
        IOpenRange,
        Reference.IOpenRange,
        IEmptyRange,
        Reference.IEmptyRange,
        IEquatable<OpenRange>
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

        public IPointSequence PointSequence() =>
            _nonEmpty
                ? new PointSequenceFromRange(Start, End, OpenStart, OpenEnd)
                : (IPointSequence) new EmptyPointSequence();
    }
}
