// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Mors.Ranges.Sequences.Tests
{
    public class RangesInSequenceTests
    {
        [Test]
        [TestCaseSource(nameof(ValidRangeCases))]
        public Range ShouldReturnExpectedRangeForValidString(string @string)
        {
            var parser = new RangesInPointSequence(new TestablePointSequence(@string, 1));
            return parser.SingleOrDefault();
        }

        [Test]
        [TestCaseSource(nameof(TextRangesWithUnexpectedInput))]
        public void ShouldThrowExceptionForUnexpectedInput(string @string)
        {
            var parser = new RangesInPointSequence(new TestablePointSequence(@string, 1));
            Assert.Throws<RangesInPointSequence.UnexpectedInputException>(
                () => parser.LastOrDefault());
        }

        [Test]
        [TestCaseSource(nameof(TextRangesWithUnexpectedEndOfInput))]
        public void ShouldThrowExceptionForUnexpectedEndOfInput(string @string)
        {
            var parser = new RangesInPointSequence(new TestablePointSequence(@string, 1));
            Assert.Throws<RangesInPointSequence.UnexpectedEndOfInputException>(
                () => parser.LastOrDefault());
        }

        public static IEnumerable<TestCaseData> ValidRangeCases
        {
            get
            {
                yield return new TestCaseData("").Returns(null);
                yield return new TestCaseData("-").Returns(null);

                yield return new TestCaseData("x").Returns(new Range(1, 1, false, false));
                yield return new TestCaseData("-x").Returns(new Range(2, 2, false, false));
                yield return new TestCaseData("x-").Returns(new Range(1, 1, false, false));
                yield return new TestCaseData("-x-").Returns(new Range(2, 2, false, false));
                yield return new TestCaseData("xx").Returns(new Range(1, 2, false, false));
                yield return new TestCaseData("-xx").Returns(new Range(2, 3, false, false));
                yield return new TestCaseData("xx-").Returns(new Range(1, 2, false, false));
                yield return new TestCaseData("-xx-").Returns(new Range(2, 3, false, false));
                yield return new TestCaseData("x=x").Returns(new Range(1, 3, false, false));
                yield return new TestCaseData("-x=x").Returns(new Range(2, 4, false, false));
                yield return new TestCaseData("x=x-").Returns(new Range(1, 3, false, false));
                yield return new TestCaseData("-x=x-").Returns(new Range(2, 4, false, false));

                yield return new TestCaseData("oo").Returns(new Range(1, 2, true, true));
                yield return new TestCaseData("-oo").Returns(new Range(2, 3, true, true));
                yield return new TestCaseData("oo-").Returns(new Range(1, 2, true, true));
                yield return new TestCaseData("-oo-").Returns(new Range(2, 3, true, true));
                yield return new TestCaseData("o=o").Returns(new Range(1, 3, true, true));
                yield return new TestCaseData("-o=o").Returns(new Range(2, 4, true, true));
                yield return new TestCaseData("o=o-").Returns(new Range(1, 3, true, true));
                yield return new TestCaseData("-o=o-").Returns(new Range(2, 4, true, true));

                yield return new TestCaseData("xo").Returns(new Range(1, 2, false, true));
                yield return new TestCaseData("-xo").Returns(new Range(2, 3, false, true));
                yield return new TestCaseData("xo-").Returns(new Range(1, 2, false, true));
                yield return new TestCaseData("-xo-").Returns(new Range(2, 3, false, true));
                yield return new TestCaseData("x=o").Returns(new Range(1, 3, false, true));
                yield return new TestCaseData("-x=o").Returns(new Range(2, 4, false, true));
                yield return new TestCaseData("x=o-").Returns(new Range(1, 3, false, true));
                yield return new TestCaseData("-x=o-").Returns(new Range(2, 4, false, true));

                yield return new TestCaseData("ox").Returns(new Range(1, 2, true, false));
                yield return new TestCaseData("-ox").Returns(new Range(2, 3, true, false));
                yield return new TestCaseData("ox-").Returns(new Range(1, 2, true, false));
                yield return new TestCaseData("-ox-").Returns(new Range(2, 3, true, false));
                yield return new TestCaseData("o=x").Returns(new Range(1, 3, true, false));
                yield return new TestCaseData("-o=x").Returns(new Range(2, 4, true, false));
                yield return new TestCaseData("o=x-").Returns(new Range(1, 3, true, false));
                yield return new TestCaseData("-o=x-").Returns(new Range(2, 4, true, false));
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
