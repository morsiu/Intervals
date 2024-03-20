using System.Linq;
using NUnit.Framework;

namespace Mors.Intervals.Generation.Tests
{
    [TestFixture]
    public sealed class TestsOfGenerationOfPairsOfIntervalsAndPoints
    {
        [Test]
        public void GeneratesPairsOfClosedIntervalsAndPointsOfAllPossibleRelations()
        {
            Assert.That(
                new PairsOfClosedIntervalsAndPointsOfAllPossibleRelations<
                    int,
                    ClosedInterval,
                    ClosedIntervals,
                    PairOfClosedIntervalAndPoint,
                    Pairs>(Enumerable.Range(1, 7).ToArray()),
                Is.EquivalentTo(new PairsOfClosedIntervalsAndPointsOfAllPossibleRelations()));
        }

        [Test]
        public void GeneratesPairsOfOpenIntervalsAndPointsOfAllPossibleRelations()
        {
            Assert.That(
                new PairsOfOpenIntervalsAndPointsOfAllPossibleRelations<
                    int,
                    OpenInterval,
                    OpenIntervals,
                    PairOfOpenIntervalAndPoint,
                    Pairs>(Enumerable.Range(1, 7).ToArray()),
                Is.EquivalentTo(new PairsOfOpenIntervalsAndPointsOfAllPossibleRelations()));
        }
    }
}
