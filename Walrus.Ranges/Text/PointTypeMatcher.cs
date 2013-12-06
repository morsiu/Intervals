// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Walrus.Ranges.Text
{
    public sealed class PointTypeMatcher
    {
        // Index values corresponds to PointType enumeration values.
        private readonly char[] characterToPointTypeLookup;

        public PointTypeMatcher(
            char uncoveredPoint,
            char coveredPoint,
            char closedEndPoint,
            char openEndPoint)
        {
            characterToPointTypeLookup = new[]
            {
                uncoveredPoint,
                coveredPoint,
                closedEndPoint,
                openEndPoint
            };
            CheckPointTypesHaveDifferentCharacters();
        }

        public PointType? Match(char character)
        {
            var pointType = LookupPointType(character);
            if (pointType == null)
            {
                return null;
            }
            return pointType.Value;
        }

        private PointType? LookupPointType(char character)
        {
            var pointType = Array.IndexOf(characterToPointTypeLookup, character);
            if (pointType == -1)
            {
                return null;
            }
            return (PointType)pointType;
        }

        private void CheckPointTypesHaveDifferentCharacters()
        {
            if (characterToPointTypeLookup.Distinct().Count() != characterToPointTypeLookup.Length)
            {
                throw new ArgumentException("There are at least two identical characters for different point types.");
            }
        }
    }
}
