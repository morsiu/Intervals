// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace Walrus.Ranges.Text.ParserStates
{
    internal sealed class InsideRangeState : ITextRangeParserState
    {
        public ITextRangeParserState Advance(Point point, RangeBuilder<int> rangeBuilder)
        {
            switch (point.Type)
            {
                case PointType.Covered:
                    return this;
                case PointType.OpenEnd:
                    rangeBuilder.SetEnd(point.Position, true);
                    return new AfterRangeState();
                case PointType.ClosedEnd:
                    rangeBuilder.SetEnd(point.Position, false);
                    return new AfterRangeState();
                case null:
                    throw new ArgumentException("The text range ended unexpectedly.");
                default:
                    throw new ArgumentException(string.Format("Point of type {0} was not expected at position {1}.", point.Type, point.Position));
            }
        }
    }
}
