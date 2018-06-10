// Copyright (C) 2013 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;
using System;
using System.Linq;
using Mors.Ranges.Generation;
using Mors.Ranges.Operations.Reference;
using Mors.Ranges.Sequences;

namespace Mors.Ranges
{
    [TestFixture]
    public class RangeOperationsTests
    {
        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNullRangePairs))]
        public void IntersectsWithShouldThrowGivenNullRange(RangePair<IRange<int>> testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.IntersectsWith(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNonNullRangePairs))]
        public void IntersectsWithShouldReturnExpectedResult(RangePair<IRange<int>> testCase)
        {
            Assert.AreEqual(
                IntersectsWithOperation.Calculate(testCase.RangeA.SequencesRange(), testCase.RangeB.SequencesRange()),
                RangeOperations.IntersectsWith(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNullRangePairs))]
        public void IntersectShouldThrowGivenNullRange(RangePair<IRange<int>> testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.Intersect(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNonNullRangePairs))]
        public void IntersectShouldReturnExpectedResult(RangePair<IRange<int>> testCase)
        {
            Assert.AreEqual(
                IntersectOperation.Calculate(
                        testCase.RangeA.SequencesRange(),
                        testCase.RangeB.SequencesRange())
                    .Range(),
                RangeOperations.Intersect(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNullRangePairs))]
        public void CoversShouldThrowGivenNullRange(RangePair<IRange<int>> testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.Covers(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNonNullRangePairs))]
        public void CoversShouldReturnExpectedResult(RangePair<IRange<int>> testCase)
        {
            Assert.AreEqual(
                CoversOperation.Calculate(testCase.RangeA.SequencesRange(), testCase.RangeB.SequencesRange()),
                RangeOperations.Covers(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNullRangePairs))]
        public void IsCoveredByShouldThrowGivenNullRange(RangePair<IRange<int>> testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.IsCoveredBy(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNonNullRangePairs))]
        public void IsCoveredByShouldReturnExpectedResult(RangePair<IRange<int>> testCase)
        {
            Assert.AreEqual(
                IsCoveredByOperation.Calculate(testCase.RangeA.SequencesRange(), testCase.RangeB.SequencesRange()),
                RangeOperations.IsCoveredBy(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNullRangePairs))]
        public void SpanShouldThrowGivenNullRange(RangePair<IRange<int>> testCase)
        {
            Assert.Throws<ArgumentNullException>(
                () => RangeOperations.Span(testCase.RangeA, testCase.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(RangePairTestCases), nameof(RangePairTestCases.AllNonNullRangePairs))]
        public void SpanShouldReturnExpectedResult(RangePair<IRange<int>> testCase)
        {
            var expected =
                new RangesInPointSequence(
                        new SpanOperation(
                            testCase.RangeA.IsEmpty
                                ? (IPointSequence) new EmptyPointSequence()
                                : new PointSequenceFromRange(
                                    testCase.RangeA.Start,
                                    testCase.RangeA.End,
                                    testCase.RangeA.HasOpenStart,
                                    testCase.RangeA.HasOpenEnd),
                            testCase.RangeB.IsEmpty
                                ? (IPointSequence) new EmptyPointSequence()
                                : new PointSequenceFromRange(
                                    testCase.RangeB.Start,
                                    testCase.RangeB.End,
                                    testCase.RangeB.HasOpenStart,
                                    testCase.RangeB.HasOpenEnd)))
                    .Select(x => Range.Create(x.Start, x.End, x.HasOpenStart, x.HasOpenEnd));
            Assert.That(
                new[] { RangeOperations.Span(testCase.RangeA, testCase.RangeB) },
                Is.EqualTo(expected.DefaultIfEmpty(Range.Empty<int>())).AsCollection);
        }
    }
}
