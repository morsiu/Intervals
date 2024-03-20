using NUnit.Framework;

namespace Mors.Intervals.Generation.Tests
{
    [TestFixture]
    public sealed class TestsOfGenerationOfIntervals
    {
        [Test]
        public void GeneratesClosedIntervalsOfAllPossibleRelations()
        {
            Assert.That(
                new PairsOfClosedIntervalsOfAllPossibleRelations<int, ClosedInterval, PairOfClosedIntervals, ClosedIntervals, Pairs>(
                    new[] { 1, 3, 5, 7 }),
                Is.EquivalentTo(new PairsOfClosedIntervalsOfAllPossibleRelations()));
        }

        [Test]
        public void GeneratesOpenIntervalsOfAllPossibleRelations()
        {
            Assert.That(
                new PairsOfOpenIntervalsOfAllPossibleRelations<OpenInterval, PairOfOpenIntervals, OpenIntervals, Pairs>(),
                Is.EquivalentTo(new PairsOfOpenIntervalsOfAllPossibleRelations()));
        }
    }
}
