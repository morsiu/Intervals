// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Mors.Ranges.Text
{
    public sealed class PointTypeCharacters
    {
        private readonly char _uncoveredPoint;
        private readonly char _coveredPoint;
        private readonly char _closedEndPoint;
        private readonly char _openEndPoint;

        public PointTypeCharacters(
            char uncoveredPoint,
            char coveredPoint,
            char closedEndPoint,
            char openEndPoint)
        {
            if (uncoveredPoint == coveredPoint ||
                uncoveredPoint == closedEndPoint ||
                uncoveredPoint == openEndPoint ||
                coveredPoint == closedEndPoint ||
                coveredPoint == openEndPoint ||
                closedEndPoint == openEndPoint)
            {
                throw new ArgumentException("There are at least two identical characters for different point types.");
            }
            _uncoveredPoint = uncoveredPoint;
            _coveredPoint = coveredPoint;
            _closedEndPoint = closedEndPoint;
            _openEndPoint = openEndPoint;
        }

        public PointType? MaybePointType(char character)
        {
            if (character == _uncoveredPoint) return Text.PointType.Uncovered;
            if (character == _coveredPoint) return Text.PointType.Covered;
            if (character == _openEndPoint) return Text.PointType.OpenEnd;
            if (character == _closedEndPoint) return Text.PointType.ClosedEnd;
            return null;
        }

        public PointTypePair PointTypePair(char pointACharacter, char pointBCharacter)
        {
            var pointA = MaybePointType(pointACharacter);
            var pointB = MaybePointType(pointBCharacter);
            return new PointTypePair(
                pointA ?? throw new ArgumentException("The character does not represent a valid point.", nameof(pointACharacter)),
                pointB ?? throw new ArgumentException("The characted does not represent a valid point.", nameof(pointBCharacter)));
        }

        public PointType PointType(char pointCharacter)
        {
            var point = MaybePointType(pointCharacter);
            return point ?? throw new ArgumentException("The character does not represent a valid point.", nameof(pointCharacter));
        }
    }
}
