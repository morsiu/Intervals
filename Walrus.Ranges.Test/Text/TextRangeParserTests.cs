// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;
using System.Collections.Generic;

namespace Walrus.Ranges.Text
{
    [TestFixture]
    public class TextRangeParserTests
    {
        [Test]
        [TestCaseSource("ValidTextRanges")]
        public IRange<int> ParseShouldReturnExpectedRange(string textRange)
        {
            var pointTypeMatcher = new PointTypeMatcher('-', '=', 'x', 'o');
            var parser = new TextRangeParser(pointTypeMatcher);
            return parser.Parse(textRange);
        }

        public static IEnumerable<TestCaseData> ValidTextRanges
        {
            get
            {
                yield return new TestCaseData("").Returns(Range.Empty<int>());
                yield return new TestCaseData("-").Returns(Range.Empty<int>());

                yield return new TestCaseData("x").Returns(Range.Closed(1, 1));
                yield return new TestCaseData("-x").Returns(Range.Closed(2, 2));
                yield return new TestCaseData("x-").Returns(Range.Closed(1, 1));
                yield return new TestCaseData("-x-").Returns(Range.Closed(2, 2));
                yield return new TestCaseData("xx").Returns(Range.Closed(1, 2));
                yield return new TestCaseData("-xx").Returns(Range.Closed(2, 3));
                yield return new TestCaseData("xx-").Returns(Range.Closed(1, 2));
                yield return new TestCaseData("-xx-").Returns(Range.Closed(2, 3));
                yield return new TestCaseData("x=x").Returns(Range.Closed(1, 3));
                yield return new TestCaseData("-x=x").Returns(Range.Closed(2, 4));
                yield return new TestCaseData("x=x-").Returns(Range.Closed(1, 3));
                yield return new TestCaseData("-x=x-").Returns(Range.Closed(2, 4));

                yield return new TestCaseData("oo").Returns(Range.Open(1, 2));
                yield return new TestCaseData("-oo").Returns(Range.Open(2, 3));
                yield return new TestCaseData("oo-").Returns(Range.Open(1, 2));
                yield return new TestCaseData("-oo-").Returns(Range.Open(2, 3));
                yield return new TestCaseData("o=o").Returns(Range.Open(1, 3));
                yield return new TestCaseData("-o=o").Returns(Range.Open(2, 4));
                yield return new TestCaseData("o=o-").Returns(Range.Open(1, 3));
                yield return new TestCaseData("-o=o-").Returns(Range.Open(2, 4));

                yield return new TestCaseData("xo").Returns(Range.RightOpen(1, 2));
                yield return new TestCaseData("-xo").Returns(Range.RightOpen(2, 3));
                yield return new TestCaseData("xo-").Returns(Range.RightOpen(1, 2));
                yield return new TestCaseData("-xo-").Returns(Range.RightOpen(2, 3));
                yield return new TestCaseData("x=o").Returns(Range.RightOpen(1, 3));
                yield return new TestCaseData("-x=o").Returns(Range.RightOpen(2, 4));
                yield return new TestCaseData("x=o-").Returns(Range.RightOpen(1, 3));
                yield return new TestCaseData("-x=o-").Returns(Range.RightOpen(2, 4));

                yield return new TestCaseData("ox").Returns(Range.LeftOpen(1, 2));
                yield return new TestCaseData("-ox").Returns(Range.LeftOpen(2, 3));
                yield return new TestCaseData("ox-").Returns(Range.LeftOpen(1, 2));
                yield return new TestCaseData("-ox-").Returns(Range.LeftOpen(2, 3));
                yield return new TestCaseData("o=x").Returns(Range.LeftOpen(1, 3));
                yield return new TestCaseData("-o=x").Returns(Range.LeftOpen(2, 4));
                yield return new TestCaseData("o=x-").Returns(Range.LeftOpen(1, 3));
                yield return new TestCaseData("-o=x-").Returns(Range.LeftOpen(2, 4));
            }
        }
    }
}
