// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;
using System;
using Walrus.Ranges.Test.Cases.Generation;
using Walrus.Ranges.Test.Cases.Generation.Operations;

namespace Walrus.Ranges
{
    [TestFixture]
    public class RangeOperationsTests
    {
        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "NullRangePairs")]
        public void IntersectsWithShouldThrowGivenNullRange(RangePairTestCases.TestCase testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.IntersectsWith(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "AllNonNullRangePairs2")]
        public void IntersectsWithShouldReturnExpectedResult(RangePair testCase)
        {
            var expected = IntersectsWithOperation.Calculate(testCase.RangeA, testCase.RangeB);
            Assert.AreEqual(
                expected,
                RangeOperations.IntersectsWith(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "NullRangePairs")]
        public void IntersectShouldThrowGivenNullRange(RangePairTestCases.TestCase testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.Intersect(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "AllNonNullRangePairs2")]
        public void IntersectShouldReturnExpectedResult(RangePair testCase)
        {
            var expected = IntersectOperation.Calculate(testCase.RangeA, testCase.RangeB);
            Assert.AreEqual(
                expected,
                RangeOperations.Intersect(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "NullRangePairs")]
        public void CoversShouldThrowGivenNullRange(RangePairTestCases.TestCase testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.Covers(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "AllNonNullRangePairs")]
        public void CoversShouldReturnExpectedResult(RangePairTestCases.TestCase testCase)
        {
            Assert.AreEqual(
                testCase.ACoversB,
                RangeOperations.Covers(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "NullRangePairs")]
        public void IsCoveredByShouldThrowGivenNullRange(RangePairTestCases.TestCase testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.IsCoveredBy(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "AllNonNullRangePairs")]
        public void IsCoveredByShouldReturnExpectedResult(RangePairTestCases.TestCase testCase)
        {
            Assert.AreEqual(
                testCase.BCoversA,
                RangeOperations.IsCoveredBy(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "NullRangePairs")]
        public void SpanShouldThrowGivenNullRange(RangePairTestCases.TestCase testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.Span(testCase.A, testCase.B));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), "AllNonNullRangePairs")]
        public void SpanShouldReturnExpectedResult(RangePairTestCases.TestCase testCase)
        {
            Assert.AreEqual(
                testCase.ABSpan,
                RangeOperations.Span(testCase.A, testCase.B));
        }

    }
}
