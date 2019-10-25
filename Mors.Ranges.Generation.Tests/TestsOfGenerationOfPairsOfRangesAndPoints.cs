using System.Linq;
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
                        int,
                        ClosedRange,
                        ClosedRanges,
                        PairOfClosedRangeAndPoint,
                        Pairs>(Enumerable.Range(1, 7).ToArray()));
        }

        [Test]
        public void GeneratesPairsOfOpenRangesAndPointsOfAllPossibleRelations()
        {
            CollectionAssert.AreEquivalent(
                expected: new PairsOfOpenRangesAndPointsOfAllPossibleRelations(),
                actual:
                    new PairsOfOpenRangesAndPointsOfAllPossibleRelations<
                        int,
                        OpenRange,
                        OpenRanges,
                        PairOfOpenRangeAndPoint,
                        Pairs>(Enumerable.Range(1, 7).ToArray()));
        }
    }
}
