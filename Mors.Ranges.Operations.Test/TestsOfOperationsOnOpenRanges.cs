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
