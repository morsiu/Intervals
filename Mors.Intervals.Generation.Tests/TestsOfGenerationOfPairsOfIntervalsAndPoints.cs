using System.Linq;
using NUnit.Framework;
using CollectionAssert = NUnit.Framework.Legacy.CollectionAssert;

namespace Mors.Intervals.Generation.Tests
{
    [TestFixture]
    public sealed class TestsOfGenerationOfPairsOfIntervalsAndPoints
    {
        [Test]
        public void GeneratesPairsOfClosedIntervalsAndPointsOfAllPossibleRelations()
        {
            CollectionAssert.AreEquivalent(
                expected: new PairsOfClosedIntervalsAndPointsOfAllPossibleRelations(),
                actual:
                    new PairsOfClosedIntervalsAndPointsOfAllPossibleRelations<
                        int,
                        ClosedInterval,
                        ClosedIntervals,
                        PairOfClosedIntervalAndPoint,
                        Pairs>(Enumerable.Range(1, 7).ToArray()));
        }

        [Test]
        public void GeneratesPairsOfOpenIntervalsAndPointsOfAllPossibleRelations()
        {
            CollectionAssert.AreEquivalent(
                expected: new PairsOfOpenIntervalsAndPointsOfAllPossibleRelations(),
                actual:
                    new PairsOfOpenIntervalsAndPointsOfAllPossibleRelations<
                        int,
                        OpenInterval,
                        OpenIntervals,
                        PairOfOpenIntervalAndPoint,
                        Pairs>(Enumerable.Range(1, 7).ToArray()));
        }
    }
}
