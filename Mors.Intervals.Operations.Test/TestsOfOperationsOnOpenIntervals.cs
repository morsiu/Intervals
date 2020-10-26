using NUnit.Framework;
using ReferenceOpenIntervalOperations = Mors.Intervals.Operations.Reference.ReferenceOpenIntervalOperations<Mors.Intervals.Operations.Test.OpenInterval, Mors.Intervals.Operations.Test.IntervalUnion<Mors.Intervals.Operations.Test.OpenInterval>, Mors.Intervals.Operations.Test.OpenIntervals, Mors.Intervals.Operations.Test.IntervalUnions<Mors.Intervals.Operations.Test.OpenInterval>>;

namespace Mors.Intervals.Operations.Test
{
    [TestFixture]
    public sealed class TestsOfOperationsOnOpenIntervals
    {
        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsAndPointsOfAllPossibleRelations))]
        public void ContainsReturnsExpectedResult((OpenInterval Interval, int Point) pair)
        {
            Assert.AreEqual(
                ReferenceOpenIntervalOperations.Contains(pair.Interval, pair.Point),
                OpenIntervalOperations.Contains(pair.Interval, pair.Point));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void IntersectsWithReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            Assert.AreEqual(
                ReferenceOpenIntervalOperations.IntersectsWith(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                OpenIntervalOperations.IntersectsWith<int, OpenInterval>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void IntersectReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            OpenIntervalOperations.Intersect<int, OpenInterval, OpenIntervals>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB, out var actual);
            Assert.AreEqual(
                ReferenceOpenIntervalOperations.Intersect(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void CoversReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            Assert.AreEqual(
                ReferenceOpenIntervalOperations.Covers(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                OpenIntervalOperations.Covers<int, OpenInterval>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void IsCoveredByReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            Assert.AreEqual(
                ReferenceOpenIntervalOperations.IsCoveredBy(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                OpenIntervalOperations.IsCoveredBy<int, OpenInterval>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB));
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void SpanReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            OpenIntervalOperations.Span<int, OpenInterval, OpenIntervals>(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB, out var actual);
            Assert.AreEqual(
                ReferenceOpenIntervalOperations.Span(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void SubtractReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            OpenIntervalOperations.Subtract<int, OpenInterval, IntervalUnion<OpenInterval>, OpenIntervals, IntervalUnions<OpenInterval>>(
                pairOfIntervals.IntervalA,
                pairOfIntervals.IntervalB,
                out var actual);
            Assert.AreEqual(
                ReferenceOpenIntervalOperations.Subtract(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                actual);
        }

        [Test]
        [TestCaseSource(typeof(PairsOfOpenIntervalsOfAllPossibleRelations))]
        public void UnionReturnsExpectedResult((OpenInterval IntervalA, OpenInterval IntervalB) pairOfIntervals)
        {
            OpenIntervalOperations.Union<int, OpenInterval, IntervalUnion<OpenInterval>, OpenIntervals, IntervalUnions<OpenInterval>>(
                pairOfIntervals.IntervalA,
                pairOfIntervals.IntervalB,
                out var actual);
            Assert.AreEqual(
                ReferenceOpenIntervalOperations.Union(pairOfIntervals.IntervalA, pairOfIntervals.IntervalB),
                actual);
        }
    }
}
