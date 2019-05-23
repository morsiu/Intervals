using NUnit.Framework;

namespace Mors.Ranges.Generation.Tests
{
    [TestFixture]
    public sealed class TestsOfGenerationOfPairsOfRangesAndPoints
    {
        [Test]
        public void GeneratesPairsOfClosedRangesAndPointsOfAllPossibleRelations()
        {
            CollectionAssert.AreEquivalent(
                expected: new PairsOfClosedRangesAndPointsOfAllPossibleRelations(),
                actual:
                    new PairsOfClosedRangesAndPointsOfAllPossibleRelations<
                        ClosedRange,
                        ClosedRanges,
                        PairOfClosedRangeAndPoint,
                        Pairs>());
        }

        [Test]
        public void GeneratesPairsOfOpenRangesAndPointsOfAllPossibleRelations()
        {
            CollectionAssert.AreEquivalent(
                expected: new PairsOfOpenRangesAndPointsOfAllPossibleRelations(),
                actual:
                    new PairsOfOpenRangesAndPointsOfAllPossibleRelations<
                        OpenRange,
                        OpenRanges,
                        PairOfOpenRangeAndPoint,
                        Pairs>());
        }
    }
}
