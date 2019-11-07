using System.Collections.Generic;
using Xunit;

namespace Mors.Intervals.Inequations.Tests
{
    public class Emptiness_of_intervals_and_inequalities
    {
        public static IEnumerable<object[]> NonEmptyOpenIntervals() =>
            new AllNonEmptyOpenIntervals().ToEnumerableOfObjects();

        public static IEnumerable<object[]> EmptyOpenIntervals() =>
            new AllEmptyOpenIntervals().ToEnumerableOfObjects();

        public static IEnumerable<object[]> NonEmptyClosedIntervals() =>
            new AllNonEmptyClosedIntervals().ToEnumerableOfObjects();

        public static IEnumerable<object[]> EmptyClosedIntervals() =>
            new AllEmptyClosedIntervals().ToEnumerableOfObjects();

        [Theory]
        [MemberData(nameof(EmptyClosedIntervals))]
        public void Inequality_from_empty_closed_interval_is_empty(ClosedInterval interval) =>
            Assert.True(
                interval.ToInequation().IsEmpty());

        [Theory]
        [MemberData(nameof(EmptyOpenIntervals))]
        public void Inequality_from_empty_open_interval_is_empty(OpenInterval interval) =>
            Assert.True(
                interval.ToInequation().IsEmpty());

        [Theory]
        [MemberData(nameof(NonEmptyClosedIntervals))]
        public void Inequality_from_non_empty_closed_interval_is_not_empty(ClosedInterval interval) =>
            Assert.False(
                interval.ToInequation().IsEmpty());

        [Theory]
        [MemberData(nameof(NonEmptyOpenIntervals))]
        public void Inequality_from_non_empty_open_interval_is_not_empty(OpenInterval interval) =>
            Assert.False(
                interval.ToInequation().IsEmpty());
    }
}