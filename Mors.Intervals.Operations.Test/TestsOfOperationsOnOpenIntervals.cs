using NUnit.Framework;
using ReferenceOpenIntervalOperations = Mors.Intervals.Operations.Reference.ReferenceOpenIntervalOperations<Mors.Intervals.Operations.Test.OpenInterval, Mors.Intervals.Operations.Test.OpenIntervalUnion, Mors.Intervals.Operations.Test.OpenIntervals, Mors.Intervals.Operations.Test.OpenIntervalUnions>;

namespace Mors.Intervals.Operations.Test
{
    [TestFixture]
    public sealed class TestsOfOperationsOnOpenIntervals
    {
        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsAndPointsOfAllPossibleRelations))]
        public void ContainsReturnsExpectedResult((OpenInterval Interval, int Point) pair)
        {
            Assert.That(
                OpenIntervalOperations.Contains(pair.Interval, pair.Point),
                Is.EqualTo(ReferenceOpenIntervalOperations.Contains(pair.Interval, pair.Point)));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void IntersectsWithReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            Assert.That(
                OpenIntervalOperations.IntersectsWith<int, OpenInterval>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                Is.EqualTo(ReferenceOpenIntervalOperations.IntersectsWith(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB)));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void IntersectReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            OpenIntervalOperations.Intersect<int, OpenInterval, OpenIntervals>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB, out var actual);
            Assert.That(
                actual,
                Is.EqualTo(ReferenceOpenIntervalOperations.Intersect(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB)));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void CoversReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            Assert.That(
                OpenIntervalOperations.Covers<int, OpenInterval>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                Is.EqualTo(ReferenceOpenIntervalOperations.Covers(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB)));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void IsCoveredByReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            Assert.That(
                OpenIntervalOperations.IsCoveredBy<int, OpenInterval>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                Is.EqualTo(ReferenceOpenIntervalOperations.IsCoveredBy(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB)));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void SpanReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            OpenIntervalOperations.Span<int, OpenInterval, OpenIntervals>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB, out var actual);
            Assert.That(
                actual,
                Is.EqualTo(ReferenceOpenIntervalOperations.Span(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB)));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void SubtractReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            OpenIntervalOperations.Subtract<int, OpenInterval, OpenIntervalUnion, OpenIntervals, OpenIntervalUnions>(
                pairOfIntervals.IntervalA,
                pairOfIntervals.IntervalB,
                out var actual);
            Assert.That(
                actual,
                Is.EqualTo(ReferenceOpenIntervalOperations.Subtract(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB)));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void UnionReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            OpenIntervalOperations.Union<int, OpenInterval, OpenIntervalUnion, OpenIntervals, OpenIntervalUnions>(
                pairOfIntervals.IntervalA,
                pairOfIntervals.IntervalB,
                out var actual);
            Assert.That(
                actual,
                Is.EqualTo(ReferenceOpenIntervalOperations.Union(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB)));
        }
    }
}
