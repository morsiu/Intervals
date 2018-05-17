// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Mors.Ranges.Test.Support.RangeOperations.StateMachines;
using Mors.Ranges.Text;

namespace Mors.Ranges.Test.Support.RangeOperations.Converters
{
    internal static class PointTypeConverter
    {
        private static readonly PointTypeCharacters Characters = new PointTypeCharacters('-', '=', 'x', 'o');

        public static PointTypePair ToPointPair(char pointACharacter, char pointBCharacter)
        {
            var pointA = Characters.PointType(pointACharacter);
            var pointB = Characters.PointType(pointBCharacter);
            return new PointTypePair(
                pointA ?? throw new ArgumentException("The character does not represent a valid point.", nameof(pointACharacter)),
                pointB ?? throw new ArgumentException("The characted does not represent a valid point.", nameof(pointBCharacter)));
        }

        public static PointType ToPoint(char pointCharacter)
        {
            var point = Characters.PointType(pointCharacter);
            return point ?? throw new ArgumentException("The character does not represent a valid point.", nameof(pointCharacter));
        }
    }
}
