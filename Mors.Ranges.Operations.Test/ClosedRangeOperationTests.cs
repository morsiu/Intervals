// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Mors.Ranges.Test.Support.RangeGeneration;
using Mors.Ranges.Test.Support.RangeOperations;
using NUnit.Framework;

namespace Mors.Ranges.Operations
{
    [TestFixture]
    public sealed class ClosedRangeOperationTests
    {
        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void IntersectsWithReturnsExpectedResult(RangePair pairOfRanges)
        {
            var expected = IntersectsWithOperation.Calculate(pairOfRanges.RangeA, pairOfRanges.RangeB);
            var rangeA = new ClosedRange(pairOfRanges.RangeA);
            var rangeB = new ClosedRange(pairOfRanges.RangeB);
            var actual = ClosedRangeOperations.IntersectsWith<int, ClosedRange>(rangeA, rangeB);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void IntersectReturnsExpectedResult(RangePair pairOfRanges)
        {
            var expected = IntersectOperation.Calculate(pairOfRanges.RangeA, pairOfRanges.RangeB);
            var rangeA = new ClosedRange(pairOfRanges.RangeA);
            var rangeB = new ClosedRange(pairOfRanges.RangeB);
            ClosedRangeOperations.Intersect<int, ClosedRange, ClosedRanges>(rangeA, rangeB, out var actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void CoversReturnsExpectedResult(RangePair pairOfRanges)
        {
            var expected = CoversOperation.Calculate(pairOfRanges.RangeA, pairOfRanges.RangeB);
            var rangeA = new ClosedRange(pairOfRanges.RangeA);
            var rangeB = new ClosedRange(pairOfRanges.RangeB);
            var actual = ClosedRangeOperations.Covers<int, ClosedRange>(rangeA, rangeB);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void IsCoveredByReturnsExpectedResult(RangePair pairOfRanges)
        {
            var expected = IsCoveredByOperation.Calculate(pairOfRanges.RangeA, pairOfRanges.RangeB);
            var rangeA = new ClosedRange(pairOfRanges.RangeA);
            var rangeB = new ClosedRange(pairOfRanges.RangeB);
            var actual = ClosedRangeOperations.IsCoveredBy<int, ClosedRange>(rangeA, rangeB);
            Assert.AreEqual(actual, expected);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void SpanReturnsExpectedResult(RangePair pairOfRanges)
        {
            var expected = SpanOperation.Calculate(pairOfRanges.RangeA, pairOfRanges.RangeB);
            var rangeA = new ClosedRange(pairOfRanges.RangeA);
            var rangeB = new ClosedRange(pairOfRanges.RangeB);
            ClosedRangeOperations.Span<int, ClosedRange, ClosedRanges>(rangeA, rangeB, out var actual);
            Assert.AreEqual(actual, expected);
        }
    }
}
