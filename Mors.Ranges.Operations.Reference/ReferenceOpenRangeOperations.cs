using System.Linq;

namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceOpenRangeOperations<TOpenRange, TOpenRanges>
        where TOpenRange : IOpenRange<int>, IEmptyRange
        where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
    {
        public static object Contains(in TOpenRange range, int point) =>
            range.ToInequation().IsSatisfiedBy(point);

        public static bool Covers(TOpenRange first, TOpenRange second)
        {
            var secondInequation = second.ToInequation();
            var firstInequation = first.ToInequation();
            return !firstInequation.IsEmpty<Integers>()
                   && !secondInequation.IsEmpty<Integers>()
                   && secondInequation.And(firstInequation.Not())
                       .ToOpenRanges<TOpenRange, TOpenRanges>()
                       .All(x => x.Empty);
        }

        public static bool IntersectsWith(TOpenRange first, TOpenRange second) =>
            first.ToInequation().And(second.ToInequation())
                .ToOpenRanges<TOpenRange, TOpenRanges>()
                .Any(x => !x.Empty);

        public static TOpenRange Intersect(TOpenRange first, TOpenRange second) =>
            first.ToInequation().And(second.ToInequation())
                .ToOpenRanges<TOpenRange, TOpenRanges>()
                .Single();

        public static bool IsCoveredBy(TOpenRange first, TOpenRange second) =>
            Covers(second, first);

        public static TOpenRange Span(TOpenRange first, TOpenRange second) =>
            first.ToInequation().Or(second.ToInequation()).Closure()
                .ToOpenRanges<TOpenRange, TOpenRanges>()
                .Single();
    }
}