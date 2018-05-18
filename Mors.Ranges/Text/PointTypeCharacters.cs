// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;

namespace Mors.Ranges.Text
{
    public sealed class PointTypeCharacters
    {
        private readonly IReadOnlyDictionary<char, PointType> _characters;

        public PointTypeCharacters(
            char uncoveredPoint,
            char coveredPoint,
            char closedEndPoint,
            char openEndPoint)
        {
            var characters =
                new Dictionary<char, PointType>
                {
                    { uncoveredPoint, Text.PointType.Uncovered },
                    { coveredPoint, Text.PointType.Covered },
                    { closedEndPoint, Text.PointType.ClosedEnd },
                    { openEndPoint, Text.PointType.OpenEnd }
                };
            if (characters.Count < 4)
            {
                throw new ArgumentException("There are at least two identical characters for different point types.");
            }
            _characters = characters;
        }

        public PointType? MaybePointType(char character)
        {
            return _characters.TryGetValue(character, out var pointType)
                ? pointType
                : default(PointType?);
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
