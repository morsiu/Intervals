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
                actual: new PairsOfClosedRangesOfAllPossibleRelations<int, ClosedRange, PairOfClosedRanges, ClosedRanges, Pairs>(
                    new[] { 1, 3, 5, 7 }));
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
