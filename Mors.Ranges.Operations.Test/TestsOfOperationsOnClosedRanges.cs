using NUnit.Framework;

namespace Mors.Ranges.Operations
{
    using ReferenceClosedRangeOperations = Reference.ReferenceClosedRangeOperations<ClosedRange, RangeUnion<ClosedRange>, ClosedRanges, RangeUnions<ClosedRange>>;

    [TestFixture]
    public sealed class TestsOfOperationsOnClosedRanges
    {
        [Test]
        [TestCaseSource(typeof(PairsOfClosedRangesOfAllPossibleRelations))]
        public void IntersectsWithReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            Assert.AreEqual(
                ReferenceClosedRangeOperations.IntersectsWith(pairOfRanges.RangeA, pairOfRanges.RangeB),
                ClosedRangeOperations.IntersectsWith<int, ClosedRange>(pairOfRanges.RangeA, pairOfRanges.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRangesAndPointsOfAllPossibleRelations))]
        public void ContainsReturnsExpectedResult((ClosedRange Range, int Point) pair)
        {
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Contains(pair.Range, pair.Point),
                ClosedRangeOperations.Contains(pair.Range, pair.Point));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRangesOfAllPossibleRelations))]
        public void IntersectReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            ClosedRangeOperations.Intersect<int, ClosedRange, ClosedRanges>(pairOfRanges.RangeA, pairOfRanges.RangeB, out var actual);
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Intersect(pairOfRanges.RangeA, pairOfRanges.RangeB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRangesOfAllPossibleRelations))]
        public void CoversReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Covers(pairOfRanges.RangeA, pairOfRanges.RangeB),
                ClosedRangeOperations.Covers<int, ClosedRange>(pairOfRanges.RangeA, pairOfRanges.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRangesOfAllPossibleRelations))]
        public void IsCoveredByReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            Assert.AreEqual(
                ReferenceClosedRangeOperations.IsCoveredBy(pairOfRanges.RangeA, pairOfRanges.RangeB),
                ClosedRangeOperations.IsCoveredBy<int, ClosedRange>(pairOfRanges.RangeA, pairOfRanges.RangeB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRangesOfAllPossibleRelations))]
        public void SpanReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            ClosedRangeOperations.Span<int, ClosedRange, ClosedRanges>(pairOfRanges.RangeA, pairOfRanges.RangeB, out var actual);
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Span(pairOfRanges.RangeA, pairOfRanges.RangeB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRangesOfAllPossibleRelations))]
        public void SubtractReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            ClosedRangeOperations.Subtract<int, ClosedRange, RangeUnion<ClosedRange>, ClosedRanges, RangeUnions<ClosedRange>>(
                pairOfRanges.RangeA,
                pairOfRanges.RangeB,
                new Integers(),
                out var actual);
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Subtract(pairOfRanges.RangeA, pairOfRanges.RangeB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedRangesOfAllPossibleRelations))]
        public void UnionReturnsExpectedResult((ClosedRange RangeA, ClosedRange RangeB) pairOfRanges)
        {
            ClosedRangeOperations.Union<int, ClosedRange, RangeUnion<ClosedRange>, ClosedRanges, RangeUnions<ClosedRange>>(
                pairOfRanges.RangeA,
                pairOfRanges.RangeB,
                out var actual);
            Assert.AreEqual(
                ReferenceClosedRangeOperations.Union(pairOfRanges.RangeA, pairOfRanges.RangeB),
                actual);
        }
    }
}
