using System.Collections.Generic;
using Xunit;

namespace Mors.Ranges.Inequations.Tests
{
    public class Emptiness_of_ranges_and_inequalities
    {
        public static IEnumerable<object[]> NonEmptyOpenRanges() =>
            new AllNonEmptyOpenRanges().ToEnumerableOfObjects();

        public static IEnumerable<object[]> EmptyOpenRanges() =>
            new AllEmptyOpenRanges().ToEnumerableOfObjects();

        public static IEnumerable<object[]> NonEmptyClosedRanges() =>
            new AllNonEmptyClosedRanges().ToEnumerableOfObjects();

        public static IEnumerable<object[]> EmptyClosedRanges() =>
            new AllEmptyClosedRanges().ToEnumerableOfObjects();

        [Theory]
        [MemberData(nameof(EmptyClosedRanges))]
        public void Inequality_from_empty_closed_range_is_empty(ClosedRange range) =>
            Assert.True(
                range.ToInequation().IsEmpty());

        [Theory]
        [MemberData(nameof(EmptyOpenRanges))]
        public void Inequality_from_empty_open_range_is_empty(OpenRange range) =>
            Assert.True(
                range.ToInequation().IsEmpty());

        [Theory]
        [MemberData(nameof(NonEmptyClosedRanges))]
        public void Inequality_from_non_empty_closed_range_is_not_empty(ClosedRange range) =>
            Assert.False(
                range.ToInequation().IsEmpty());

        [Theory]
        [MemberData(nameof(NonEmptyOpenRanges))]
        public void Inequality_from_non_empty_open_range_is_not_empty(OpenRange range) =>
            Assert.False(
                range.ToInequation().IsEmpty());
    }
}