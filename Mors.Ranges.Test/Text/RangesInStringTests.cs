// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Text
{
    [TestFixture]
    public class RangesInStringTests
    {
        [Test]
        [TestCaseSource(nameof(ValidTextRangeCases))]
        public IRange<int> ShouldReturnExpectedRangeForValidString(string @string)
        {
            var characters = new PointTypeCharacters('-', '=', 'x', 'o');
            var parser = new RangesInString(@string, characters);
            return parser.SingleOrDefault() ?? Range.Empty<int>();
        }

        [Test]
        [TestCaseSource(nameof(TextRangesWithUnexpectedInput))]
        public void ShouldThrowExceptionForUnexpectedInput(string @string)
        {
            var characters = new PointTypeCharacters('-', '=', 'x', 'o');
            var parser = new RangesInString(@string, characters);
            Assert.Throws<RangesInString.UnexpectedInputException>(
                () => parser.LastOrDefault());
        }

        [Test]
        [TestCaseSource(nameof(TextRangesWithUnexpectedEndOfInput))]
        public void ShouldThrowExceptionForUnexpectedEndOfInput(string @string)
        {
            var characters = new PointTypeCharacters('-', '=', 'x', 'o');
            var parser = new RangesInString(@string, characters);
            Assert.Throws<RangesInString.UnexpectedEndOfInputException>(
                () => parser.LastOrDefault());
        }

        public static IEnumerable<TestCaseData> ValidTextRangeCases
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

        public static IEnumerable<string> TextRangesWithUnexpectedInput
        {
            get
            {
                yield return "-=";
                yield return "=";
                yield return "x-=";
                yield return "x=-";
                yield return "x=x=";
                yield return "x=o=";
                yield return "o-=";
                yield return "o-x";
                yield return "o-o";
                yield return "o=-";
                yield return "o=x=";
                yield return "o=o=";
            }
        }

        public static IEnumerable<string> TextRangesWithUnexpectedEndOfInput
        {
            get
            {
                yield return "x-o";
                yield return "x=";
                yield return "o=";
            }
        }
    }
}
