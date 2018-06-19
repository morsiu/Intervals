// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;

namespace Mors.Ranges.Operations
{
    [TestFixture]
    public sealed class ClosedRangeOperationTests
    {
        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void IntersectsWithReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            Assert.AreEqual(
                ReferenceClosedRangeOperations.IntersectsWith(pairOfRanges.RangeA, pairOfRanges.RangeB),
                ClosedRangeOperations.IntersectsWith<int, ClosedRange>(pairOfRanges.RangeA, pairOfRanges.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(AllPossibleRelationsOfClosedRangeAndPoint))]
        public void ContainsReturnsExpectedResult((ClosedRange Range, int Point) pair)
        {
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Contains(pair.Range, pair.Point),
                ClosedRangeOperations.Contains(pair.Range, pair.Point));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void IntersectReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            ClosedRangeOperations.Intersect<int, ClosedRange, ClosedRanges>(pairOfRanges.RangeA, pairOfRanges.RangeB, out var actual);
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Intersect(pairOfRanges.RangeA, pairOfRanges.RangeB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void CoversReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Covers(pairOfRanges.RangeA, pairOfRanges.RangeB),
                ClosedRangeOperations.Covers<int, ClosedRange>(pairOfRanges.RangeA, pairOfRanges.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void IsCoveredByReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            Assert.AreEqual(
                ReferenceClosedRangeOperations.IsCoveredBy(pairOfRanges.RangeA, pairOfRanges.RangeB),
                ClosedRangeOperations.IsCoveredBy<int, ClosedRange>(pairOfRanges.RangeA, pairOfRanges.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRanges), nameof(PairsOfClosedRanges.OfAllPossibleRelations))]
        public void SpanReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            ClosedRangeOperations.Span<int, ClosedRange, ClosedRanges>(pairOfRanges.RangeA, pairOfRanges.RangeB, out var actual);
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Span(pairOfRanges.RangeA, pairOfRanges.RangeB),
                actual);
        }
    }
}
