// Copyright (C) 2018 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Collections.Generic;
using NUnit.Framework;

namespace Mors.Ranges.Sequences.Tests
{
    public class PointSequenceFromRangeTests
    {
        public static IEnumerable<TestCaseData> EnumerationTestCases()
        {
            yield return new TestCaseData(new Range(0, 0, true, true)).Returns(new TestablePointSequence("-", 0));
            yield return new TestCaseData(new Range(0, 0, false, true)).Returns(new TestablePointSequence("-", 0));
            yield return new TestCaseData(new Range(0, 0, true, false)).Returns(new TestablePointSequence("-", 0));
            yield return new TestCaseData(new Range(0, 0, false, false)).Returns(new TestablePointSequence("#", 0));
            yield return new TestCaseData(new Range(0, 1, false, false)).Returns(new TestablePointSequence("[]", 0));
            yield return new TestCaseData(new Range(0, 1, true, false)).Returns(new TestablePointSequence("(]", 0));
            yield return new TestCaseData(new Range(0, 1, false, true)).Returns(new TestablePointSequence("[)", 0));
            yield return new TestCaseData(new Range(0, 1, true, true)).Returns(new TestablePointSequence("()", 0));
            yield return new TestCaseData(new Range(0, 2, false, false)).Returns(new TestablePointSequence("[=]", 0));
            yield return new TestCaseData(new Range(0, 2, true, false)).Returns(new TestablePointSequence("(=]", 0));
            yield return new TestCaseData(new Range(0, 2, false, true)).Returns(new TestablePointSequence("[=)", 0));
            yield return new TestCaseData(new Range(0, 2, true, true)).Returns(new TestablePointSequence("(=)", 0));
        }

        [Test]
        [TestCaseSource(nameof(EnumerationTestCases))]
        public TestablePointSequence EnumerationTest(in Range input)
        {
            return new TestablePointSequence(
                new PointSequenceFromRange(input.Start, input.End, input.HasOpenStart, input.HasOpenEnd));
        }
    }
}
