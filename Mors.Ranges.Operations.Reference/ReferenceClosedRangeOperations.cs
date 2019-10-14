using System.Linq;
using Mors.Ranges.Inequations;

namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceClosedRangeOperations<TClosedRange, TClosedRanges>
        where TClosedRange : IRange<int>, IEmptyRange
        where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
    {
        public static object Contains(in TClosedRange range, int point) =>
            range.ToInequationFromClosed().IsSatisfiedBy(point);

        public static bool Covers(in TClosedRange first, in TClosedRange second) =>
            !first.Empty
            && !second.Empty
            && Inequation.And(
                    second.ToInequationFromClosed(),
                    Inequation.Not(
                        first.ToInequationFromClosed()))
                .ToClosedRanges<TClosedRange, TClosedRanges>()
                .All(x => x.Empty);

        public static bool IntersectsWith(TClosedRange first, TClosedRange second) =>
            Inequation.And(
                    first.ToInequationFromClosed(),
                    second.ToInequationFromClosed())
                .ToClosedRanges<TClosedRange, TClosedRanges>()
                .Any(x => !x.Empty);

        public static TClosedRange Intersect(TClosedRange first, TClosedRange second) =>
            Inequation.And(
                    first.ToInequationFromClosed(),
                    second.ToInequationFromClosed())
                .ToClosedRanges<TClosedRange, TClosedRanges>()
                .Single();

        public static bool IsCoveredBy(TClosedRange first, TClosedRange second) =>
            Covers(second, first);

        public static TClosedRange Span(TClosedRange first, TClosedRange second) =>
            Inequation.Closure(
                    Inequation.Or(
                        first.ToInequationFromClosed(),
                        second.ToInequationFromClosed()))
                .ToClosedRanges<TClosedRange, TClosedRanges>()
                .Single();
    }
}
