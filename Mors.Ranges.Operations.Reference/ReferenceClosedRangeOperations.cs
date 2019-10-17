using System.Linq;

namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceClosedRangeOperations<TClosedRange, TClosedRanges>
        where TClosedRange : IClosedRange<int>, IEmptyRange
        where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
    {
        public static object Contains(in TClosedRange range, int point) =>
            range.ToInequation().IsSatisfiedBy(point);

        public static bool Covers(in TClosedRange first, in TClosedRange second)
        {
            var secondInequation = second.ToInequation();
            var firstInequation = first.ToInequation();
            return !firstInequation.IsEmpty<Integers>()
                   && !secondInequation.IsEmpty<Integers>()
                   && secondInequation.And(firstInequation.Not())
                       .ToClosedRanges<TClosedRange, TClosedRanges>()
                       .All(x => x.Empty);
        }

        public static bool IntersectsWith(TClosedRange first, TClosedRange second) =>
            first.ToInequation().And(second.ToInequation())
                .ToClosedRanges<TClosedRange, TClosedRanges>()
                .Any(x => !x.Empty);

        public static TClosedRange Intersect(TClosedRange first, TClosedRange second) =>
            first.ToInequation().And(second.ToInequation())
                .ToClosedRanges<TClosedRange, TClosedRanges>()
                .Single();

        public static bool IsCoveredBy(TClosedRange first, TClosedRange second) =>
            Covers(second, first);

        public static TClosedRange Span(TClosedRange first, TClosedRange second) =>
            first.ToInequation().Or(second.ToInequation()).Closure()
                .ToClosedRanges<TClosedRange, TClosedRanges>()
                .Single();
    }
}
