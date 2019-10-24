using System.Collections.Generic;
using System.Linq;
using Mors.Ranges.Generation;
using Xunit;

namespace Mors.Ranges.Inequations.Tests
{
    public class Ranges_round_trip_through_inequalities
    {
        public static IEnumerable<object[]> NonEmptyOpenRanges() =>
            new AllNonEmptyOpenRanges<OpenRange, OpenRanges>()
                .Select(x => new object[] { x });

        public static IEnumerable<object[]> EmptyOpenRanges() =>
            new AllEmptyOpenRanges<OpenRange, OpenRanges>()
                .Select(x => new object[] { x });

        public static IEnumerable<object[]> ClosedRanges() =>
            new AllClosedRanges<ClosedRange, ClosedRanges>()
                .Select(x => new object[] { x });

        [Theory]
        [MemberData(nameof(NonEmptyOpenRanges))]
        public void Non_empty_range_round_trips_through_inequalities(OpenRange range) =>
            Assert.Equal(
                range.ToUnion(),
                range.ToInequation().ToOpenRangeUnion());

        [Theory]
        [MemberData(nameof(EmptyOpenRanges))]
        public void Empty_range_round_trips_through_inequalities(OpenRange range) =>
            Assert.Equal(
                OpenRange.Empty(),
                range.ToInequation().ToOpenRangeUnion());

        [Theory]
        [MemberData(nameof(ClosedRanges))]
        public void Closed_range_round_trips_through_inequalities(ClosedRange range) =>
            Assert.Equal(
                range.ToUnion(),
                range.ToInequation().ToClosedRangeUnion());
    }
}