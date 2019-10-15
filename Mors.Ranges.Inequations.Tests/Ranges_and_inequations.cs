using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mors.Ranges.Inequations.Tests
{
    using static Inequation;
    using static OpenRange;

    public class Ranges_and_inequations
    {
        private static IEnumerable<(OpenRangeUnion RangeUnion, Inequation Inequation)> EquivalentRangesAndInequations()
        {
            yield return (
                Closed(1, 1),
                Equal(1));
            yield return (
                Closed(1, 3),
                And(
                    GreaterThanOrEqualTo(1),
                    LessThanOrEqualTo(3)));
            yield return (
                Open(1, 3),
                And(
                    GreaterThan(1),
                    LessThan(3)));
            yield return (
                LeftOpen(1, 3),
                And(
                    GreaterThan(1),
                    LessThanOrEqualTo(3)));
            yield return (
                RightOpen(1, 3),
                And(
                    GreaterThanOrEqualTo(1),
                    LessThan(3)));
            yield return (
                Empty(),
                False());
            yield return (
                Union(
                    Closed(1, 3),
                    Closed(5, 7)),
                Or(
                    And(
                        GreaterThanOrEqualTo(1),
                        LessThanOrEqualTo(3)),
                    And(
                        GreaterThanOrEqualTo(5),
                        LessThanOrEqualTo(7))));
            yield return (
                Union(
                    RightOpen(1, 3),
                    LeftOpen(3, 5)),
                Or(
                    And(
                        GreaterThanOrEqualTo(1),
                        LessThan(3)),
                    And(
                        GreaterThan(3),
                        LessThanOrEqualTo(5))));
        }

        public static IEnumerable<(Inequation Inequation, OpenRangeUnion RangeUnion)> InequationsEquivalentToRanges()
        {
            yield return (
                And(
                    GreaterThanOrEqualTo(1),
                    LessThanOrEqualTo(1)),
                Closed(1, 1));
            yield return (
                And(
                    GreaterThan(1),
                    LessThan(1)),
                Empty());
            yield return (
                And(
                    GreaterThan(1),
                    LessThanOrEqualTo(1)),
                Empty());
            yield return (
                And(
                    LessThan(1),
                    GreaterThanOrEqualTo(1)),
                Empty());
            yield return (
                And(
                    GreaterThan(1),
                    LessThan(2)),
                Empty());
            yield return (
                Not(
                    LessThanOrEqualTo(1)),
                LeftOpen(1, Point.Maximum));
            yield return (
                Not(
                    LessThan(1)),
                Closed(1, Point.Maximum));
            yield return (
                Not(
                    GreaterThanOrEqualTo(1)),
                RightOpen(Point.Minimum, 1));
            yield return (
                Not(
                    GreaterThan(1)),
                Closed(Point.Minimum, 1));
            yield return (
                Not(
                    False()),
                Closed(Point.Minimum, Point.Maximum));
            yield return (
                Not(
                    Equal(1)),
                Union(
                    RightOpen(Point.Minimum, 1),
                    LeftOpen(1, Point.Maximum)));
            yield return (
                And(
                    GreaterThanOrEqualTo(1),
                    LessThanOrEqualTo(6)),
                Union(
                    Closed(1, 3),
                    Closed(4, 6)));
            yield return (
                And(
                    GreaterThanOrEqualTo(1),
                    LessThanOrEqualTo(5)),
                Union(
                    Closed(1, 3),
                    Closed(3, 5)));
            yield return (
                And(
                    GreaterThanOrEqualTo(1),
                    LessThanOrEqualTo(5)),
                Union(
                    RightOpen(1, 3),
                    Closed(3, 5)));
            yield return (
                And(
                    GreaterThanOrEqualTo(1),
                    LessThanOrEqualTo(5)),
                Union(
                    Closed(1, 3),
                    LeftOpen(3, 5)));
            yield return (
                And(
                    GreaterThanOrEqualTo(1),
                    LessThanOrEqualTo(5)),
                Union(
                    Closed(1, 4),
                    Closed(3, 5)));
            yield return (
                And(
                    GreaterThanOrEqualTo(1),
                    LessThanOrEqualTo(5)),
                Union(
                    Closed(1, 3),
                    Closed(4, 5)));
            yield return (
                Closure(
                    Equal(1)),
                Closed(1, 1));
            yield return (
                Closure(
                    Or(
                        And(
                            GreaterThanOrEqualTo(1),
                            LessThanOrEqualTo(3)),
                        And(
                            GreaterThanOrEqualTo(5),
                            LessThanOrEqualTo(7)))),
                Closed(1, 7));
            yield return (
                Closure(
                    And(
                        GreaterThanOrEqualTo(1),
                        LessThanOrEqualTo(3))),
                Closed(1, 3));
            yield return (
                Closure(
                    And(
                        GreaterThan(1),
                        LessThanOrEqualTo(3))),
                LeftOpen(1, 3));
            yield return (
                Closure(
                    And(
                        GreaterThanOrEqualTo(1),
                        LessThan(3))),
                RightOpen(1, 3));
            yield return (
                Closure(
                    And(
                        GreaterThan(1),
                        LessThan(3))),
                Open(1, 3));
            yield return (
                Closure(
                    False()),
                Empty());
            yield return (
                Closure(
                    Not(
                        False())),
                Closed(Point.Minimum, Point.Maximum));
       }

        [Theory]
        [MemberData(nameof(InequationsEquivalentToRangeUnions))]
        public void Inequation_is_equivalent_to_range_union(in Inequation inequation, in OpenRangeUnion rangeUnion)
        {
            Assert.Equal(rangeUnion, inequation.ToOpenRangeUnion());
        }

        public static IEnumerable<object[]> InequationsEquivalentToRangeUnions()
        {
            return EquivalentRangesAndInequations().Select(a => new object[] { a.Inequation, a.RangeUnion })
                .Concat(InequationsEquivalentToRanges().Select(a => new object[] { a.Inequation, a.RangeUnion }));
        }

        [Theory]
        [MemberData(nameof(RangesUnionsEquivalentToInequations))]
        public void Range_union_is_equivalent_to_inequation(in OpenRangeUnion rangeUnion, in Inequation inequation)
        {
            Assert.Equal(inequation, rangeUnion.ToInequation());
        }

        public static IEnumerable<object[]> RangesUnionsEquivalentToInequations()
        {
            return EquivalentRangesAndInequations().Select(a => new object[] { a.RangeUnion, a.Inequation });
        }
    }
}