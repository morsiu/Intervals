using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mors.Ranges.Inequations.Tests
{
    public class Ranges_round_trip_through_inequalities
    {
        public static IEnumerable<object[]> NonEmptyOpenRanges() =>
            new AllNonEmptyOpenRanges().ToEnumerableOfObjects();

        public static IEnumerable<object[]> EmptyOpenRanges() =>
            new AllEmptyOpenRanges().ToEnumerableOfObjects();

        public static IEnumerable<object[]> ClosedRanges() =>
            new AllEmptyClosedRanges()
                .Concat(new AllNonEmptyClosedRanges())
                .ToEnumerableOfObjects();

        [Theory]
        [MemberData(nameof(NonEmptyOpenRanges))]
        public void Non_empty_open_range_round_trips_through_inequalities(OpenRange range) =>
            Assert.Equal(
                range,
                range.ToInequation().ToOpenRangeUnion());

        [Theory]
        [MemberData(nameof(EmptyOpenRanges))]
        public void Empty_open_range_round_trips_through_inequalities(OpenRange range) =>
            Assert.Equal(
                OpenRange.Empty(),
                range.ToInequation().ToOpenRangeUnion());

        [Theory]
        [MemberData(nameof(ClosedRanges))]
        public void Closed_range_round_trips_through_inequalities(ClosedRange range) =>
            Assert.Equal(
                range,
                range.ToInequation().ToClosedRangeUnion());
    }
}