using NUnit.Framework;

namespace Mors.Intervals.Generation.Tests
{
    [TestFixture]
    public sealed class TestsOfGenerationOfIntervals
    {
        [Test]
        public void GeneratesClosedIntervalsOfAllPossibleRelations()
        {
            CollectionAssert.AreEquivalent(
                expected: new PairsOfClosedIntervalsOfAllPossibleRelations(),
                actual: new PairsOfClosedIntervalsOfAllPossibleRelations<int, ClosedInterval, PairOfClosedIntervals, ClosedIntervals, Pairs>(
                    new[] { 1, 3, 5, 7 }));
        }

        [Test]
        public void GeneratesOpenIntervalsOfAllPossibleRelations()
        {
            CollectionAssert.AreEquivalent(
                expected: new PairsOfOpenIntervalsOfAllPossibleRelations(),
                actual: new PairsOfOpenIntervalsOfAllPossibleRelations<OpenInterval, PairOfOpenIntervals, OpenIntervals, Pairs>());
        }
    }
}
