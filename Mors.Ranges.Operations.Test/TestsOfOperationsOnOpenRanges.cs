// Copyright (C) 2017 Łukasz Mrozek
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using NUnit.Framework;

namespace Mors.Ranges.Operations
{
    using ReferenceOpenRangeOperations = Reference.ReferenceOpenRangeOperations<OpenRange, OpenRanges>;

    [TestFixture]
    public sealed class TestsOfOperationsOnOpenRanges
    {
        [Test]
        [TestCaseSource(typeof(PairsOfOpenRangesAndPointsOfAllPossibleRelations))]
        public void ContainsReturnsExpectedResult((OpenRange Range, int Point) pair)
        {
            Assert.AreEqual(
                ReferenceOpenRangeOperations.Contains(pair.Range, pair.Point),
                OpenRangeOperations.Contains(pair.Range, pair.Point));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenRangesOfAllPossibleRelations))]
        public void IntersectsWithReturnsExpectedResult((OpenRange RangeA, OpenRange RangeB) pairOfRanges)
        {
            Assert.AreEqual(
                ReferenceOpenRangeOperations.IntersectsWith(pairOfRanges.RangeA, pairOfRanges.RangeB),
                OpenRangeOperations.IntersectsWith<int, OpenRange>(pairOfRanges.RangeA, pairOfRanges.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenRangesOfAllPossibleRelations))]
        public void IntersectReturnsExpectedResult((OpenRange RangeA, OpenRange RangeB) pairOfRanges)
        {
            OpenRangeOperations.Intersect<int, OpenRange, OpenRanges>(pairOfRanges.RangeA, pairOfRanges.RangeB, out var actual);
            Assert.AreEqual(
                ReferenceOpenRangeOperations.Intersect(pairOfRanges.RangeA, pairOfRanges.RangeB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenRangesOfAllPossibleRelations))]
        public void CoversReturnsExpectedResult((OpenRange RangeA, OpenRange RangeB) pairOfRanges)
        {
            Assert.AreEqual(
                ReferenceOpenRangeOperations.Covers(pairOfRanges.RangeA, pairOfRanges.RangeB),
                OpenRangeOperations.Covers<int, OpenRange>(pairOfRanges.RangeA, pairOfRanges.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenRangesOfAllPossibleRelations))]
        public void IsCoveredByReturnsExpectedResult((OpenRange RangeA, OpenRange RangeB) pairOfRanges)
        {
            Assert.AreEqual(
                ReferenceOpenRangeOperations.IsCoveredBy(pairOfRanges.RangeA, pairOfRanges.RangeB),
                OpenRangeOperations.IsCoveredBy<int, OpenRange>(pairOfRanges.RangeA, pairOfRanges.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenRangesOfAllPossibleRelations))]
        public void SpanReturnsExpectedResult((OpenRange RangeA, OpenRange RangeB) pairOfRanges)
        {
            OpenRangeOperations.Span<int, OpenRange, OpenRanges>(pairOfRanges.RangeA, pairOfRanges.RangeB, out var actual);
            Assert.AreEqual(
                ReferenceOpenRangeOperations.Span(pairOfRanges.RangeA, pairOfRanges.RangeB),
                actual);
        }
    }
}
