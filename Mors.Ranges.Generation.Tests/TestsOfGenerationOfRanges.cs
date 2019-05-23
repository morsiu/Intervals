using NUnit.Framework;

namespace Mors.Ranges.Generation.Tests
{
    [TestFixture]
    public sealed class TestsOfGenerationOfRanges
    {
        [Test]
        public void GeneratesClosedRangesOfAllPossibleRelations()
        {
            CollectionAssert.AreEquivalent(
                expected: new PairsOfClosedRangesOfAllPossibleRelations(),
                actual: new PairsOfClosedRangesOfAllPossibleRelations<ClosedRange, PairOfClosedRanges, ClosedRanges, Pairs>());
        }

        [Test]
        public void GeneratesOpenRangesOfAllPossibleRelations()
        {
            CollectionAssert.AreEquivalent(
                expected: new PairsOfOpenRangesOfAllPossibleRelations(),
                actual: new PairsOfOpenRangesOfAllPossibleRelations<OpenRange, PairOfOpenRanges, OpenRanges, Pairs>());
        }
    }
}
