using System;

namespace Mors.Ranges.Sequences
{
    public static class PointTypeCharacters
    {
        public static char Character(PointType pointType)
        {
            switch (pointType)
            {
                case Sequences.PointType.Outside: return '-';
                case Sequences.PointType.OpenStart: return '(';
                case Sequences.PointType.ClosedStart: return '[';
                case Sequences.PointType.ClosedStartAndEnd: return '#';
                case Sequences.PointType.Inside: return '=';
                case Sequences.PointType.OpenEnd: return ')';
                case Sequences.PointType.ClosedEnd: return ']';
                default: throw new ArgumentOutOfRangeException(nameof(pointType), pointType, null);
            }
        }

        public static PointType? MaybePointType(char pointType)
        {
            switch (pointType)
            {
                case '-': return Sequences.PointType.Outside;
                case '(': return Sequences.PointType.OpenStart;
                case '[': return Sequences.PointType.ClosedStart;
                case '#': return Sequences.PointType.ClosedStartAndEnd;
                case '=': return Sequences.PointType.Inside;
                case ')': return Sequences.PointType.OpenEnd;
                case ']': return Sequences.PointType.ClosedEnd;
                default: return null;
            }
        }

        public static PairOfPointTypes PairOfPointTypes(char pointTypeA, char pointTypeB)
        {
            return new PairOfPointTypes(
                MaybePointType(pointTypeA)
                    ?? throw new ArgumentOutOfRangeException(nameof(pointTypeA), pointTypeA, null),
                MaybePointType(pointTypeB)
                    ?? throw new ArgumentOutOfRangeException(nameof(pointTypeB), pointTypeB, null));
        }

        public static PointType PointType(char pointType)
        {
            return MaybePointType(pointType)
                   ?? throw new ArgumentOutOfRangeException(nameof(pointType), pointType, null);
        }
    }
}
