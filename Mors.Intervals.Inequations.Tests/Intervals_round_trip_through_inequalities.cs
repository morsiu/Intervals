using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mors.Intervals.Inequations.Tests
{
    public class Intervals_round_trip_through_inequalities
    {
        public static IEnumerable<object[]> NonEmptyOpenIntervals() =>
            new AllNonEmptyOpenIntervals().ToEnumerableOfObjects();

        public static IEnumerable<object[]> EmptyOpenIntervals() =>
            new AllEmptyOpenIntervals().ToEnumerableOfObjects();

        public static IEnumerable<object[]> ClosedIntervals() =>
            new AllEmptyClosedIntervals()
                .Concat(new AllNonEmptyClosedIntervals())
                .ToEnumerableOfObjects();

        [Theory]
        [MemberData(nameof(NonEmptyOpenIntervals))]
        public void Non_empty_open_interval_round_trips_through_inequalities(OpenInterval interval) =>
            Assert.Equal(
                interval,
                interval.ToInequation().ToOpenIntervalUnion());

        [Theory]
        [MemberData(nameof(EmptyOpenIntervals))]
        public void Empty_open_interval_round_trips_through_inequalities(OpenInterval interval) =>
            Assert.Equal(
                OpenInterval.Empty(),
                interval.ToInequation().ToOpenIntervalUnion());

        [Theory]
        [MemberData(nameof(ClosedIntervals))]
        public void Closed_interval_round_trips_through_inequalities(ClosedInterval interval) =>
            Assert.Equal(
                interval,
                interval.ToInequation().ToClosedIntervalUnion());
    }
}