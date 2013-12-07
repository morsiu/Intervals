// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using Walrus.Ranges.Text.ParserStates;

namespace Walrus.Ranges.Text
{
    public sealed class TextRangeParser
    {
        private readonly PointTypeMatcher pointTypeMatcher;

        public TextRangeParser(PointTypeMatcher pointTypeMatcher)
        {
            if (pointTypeMatcher == null) throw new ArgumentNullException("pointTypeMatcher");
            this.pointTypeMatcher = pointTypeMatcher;
        }

        public IRange<int> Parse(string textRange)
        {
            var rangeBuilder = new RangeBuilder<int>();
            ITextRangeParserState state = new BeforeRangeState();
            var textRangeEnumerator = textRange.GetEnumerator();
            var atTextRangeEnd = !textRangeEnumerator.MoveNext();
            int pointPosition = 1;
            foreach (var character in textRange)
            {
                var pointType = pointTypeMatcher.Match(character);
                if (pointType == null) throw new ArgumentException(string.Format("Character {0} in text range is not assigned to any point type.", character));
                var point = new Point(pointType.Value, pointPosition);
                state = state.Advance(point, rangeBuilder);
                pointPosition += 1;
            }
            state.Advance(new Point(null, pointPosition), rangeBuilder);
            return rangeBuilder.Build();
        }
    }
}
