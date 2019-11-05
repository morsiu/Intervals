using Mors.Ranges.Inequations;

namespace Mors.Ranges.Operations.Reference
{
    internal static class ReferenceOperation
    {
        public static bool Contains(Inequation<int> range, int point) =>
            range.IsSatisfiedBy(point);

        public static bool Covers(
            Inequation<int> first,
            Inequation<int> second) =>
            !first.IsEmpty<Integers>()
            && !second.IsEmpty<Integers>()
            && second.And(first.Not()).IsEmpty<Integers>();

        public static bool IntersectsWith(
            Inequation<int> first,
            Inequation<int> second) =>
            !first.And(second).IsEmpty<Integers>();

        public static Inequation<int> Intersect(
            Inequation<int> first,
            Inequation<int> second) =>
            first.And(second);

        public static Inequation<int> Span(
            Inequation<int> first,
            Inequation<int> second) =>
            first.Or(second).Closure();

        public static Inequation<int> Subtract(
            Inequation<int> first,
            Inequation<int> second) =>
            first.And(second.Not());

        public static Inequation<int> Union(
            Inequation<int> first,
            Inequation<int> second) =>
            first.Or(second);
    }
}