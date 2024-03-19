using NUnit.Framework;
using Assert = NUnit.Framework.Legacy.ClassicAssert;
using ReferenceClosedIntervalOperations = Mors.Intervals.Operations.Reference.ReferenceClosedIntervalOperations<Mors.Intervals.Operations.Test.ClosedInterval, Mors.Intervals.Operations.Test.IntervalUnion<Mors.Intervals.Operations.Test.ClosedInterval>, Mors.Intervals.Operations.Test.ClosedIntervals, Mors.Intervals.Operations.Test.IntervalUnions<Mors.Intervals.Operations.Test.ClosedInterval>>;

namespace Mors.Intervals.Operations.Test
{
    [TestFixture]
    public sealed class TestsOfOperationsOnClosedIntervals
    {
        [Test]
        [TestCaseSource(typeof(PairsOfClosedIntervalsOfAllPossibleRelations))]
        public void IntersectsWithReturnsExpectedResult((ClosedInterval IntervalA, ClosedInterval IntervalB) pairOfIntervals)
        {
            Assert.AreEqual(
                ReferenceClosedIntervalOperations.IntersectsWith(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                ClosedIntervalOperations.IntersectsWith<int, ClosedInterval>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedIntervalsAndPointsOfAllPossibleRelations))]
        public void ContainsReturnsExpectedResult((ClosedInterval Interval, int Point) pair)
        {
            Assert.AreEqual(
                ReferenceClosedIntervalOperations.Contains(pair.Interval, pair.Point),
                ClosedIntervalOperations.Contains(pair.Interval, pair.Point));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedIntervalsOfAllPossibleRelations))]
        public void IntersectReturnsExpectedResult((ClosedInterval IntervalA, ClosedInterval IntervalB) pairOfIntervals)
        {
            ClosedIntervalOperations.Intersect<int, ClosedInterval, ClosedIntervals>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB, out var actual);
            Assert.AreEqual(
                ReferenceClosedIntervalOperations.Intersect(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedIntervalsOfAllPossibleRelations))]
        public void CoversReturnsExpectedResult((ClosedInterval IntervalA, ClosedInterval IntervalB) pairOfIntervals)
        {
            Assert.AreEqual(
                ReferenceClosedIntervalOperations.Covers(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                ClosedIntervalOperations.Covers<int, ClosedInterval>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedIntervalsOfAllPossibleRelations))]
        public void IsCoveredByReturnsExpectedResult((ClosedInterval IntervalA, ClosedInterval IntervalB) pairOfIntervals)
        {
            Assert.AreEqual(
                ReferenceClosedIntervalOperations.IsCoveredBy(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                ClosedIntervalOperations.IsCoveredBy<int, ClosedInterval>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedIntervalsOfAllPossibleRelations))]
        public void SpanReturnsExpectedResult((ClosedInterval IntervalA, ClosedInterval IntervalB) pairOfIntervals)
        {
            ClosedIntervalOperations.Span<int, ClosedInterval, ClosedIntervals>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB, out var actual);
            Assert.AreEqual(
                ReferenceClosedIntervalOperations.Span(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedIntervalsOfAllPossibleRelations))]
        public void SubtractReturnsExpectedResult((ClosedInterval IntervalA, ClosedInterval IntervalB) pairOfIntervals)
        {
            ClosedIntervalOperations.Subtract<int, ClosedInterval, IntervalUnion<ClosedInterval>, ClosedIntervals, IntervalUnions<ClosedInterval>>(
                pairOfIntervals.IntervalA,
                pairOfIntervals.IntervalB,
                new Integers(),
                out var actual);
            Assert.AreEqual(
                ReferenceClosedIntervalOperations.Subtract(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfClosedIntervalsOfAllPossibleRelations))]
        public void UnionReturnsExpectedResult((ClosedInterval IntervalA, ClosedInterval IntervalB) pairOfIntervals)
        {
            ClosedIntervalOperations.Union<int, ClosedInterval, IntervalUnion<ClosedInterval>, ClosedIntervals, IntervalUnions<ClosedInterval>>(
                pairOfIntervals.IntervalA,
                pairOfIntervals.IntervalB,
                out var actual);
            Assert.AreEqual(
                ReferenceClosedIntervalOperations.Union(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                actual);
        }
    }
}
