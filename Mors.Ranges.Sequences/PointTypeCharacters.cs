// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
