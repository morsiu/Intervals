﻿using System.Linq;
using Mors.Ranges.Inequations;

namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceOpenRangeOperations<TOpenRange, TOpenRanges>
        where TOpenRange : IRange<int>, IOpenRange, IEmptyRange
        where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
    {
        public static object Contains(in TOpenRange range, int point) =>
            range.ToInequationFromOpen().IsSatisfiedBy(point);

        public static bool Covers(TOpenRange first, TOpenRange second) =>
            !first.Empty
            && !second.Empty
            && second.ToInequationFromOpen().And(first.ToInequationFromOpen().Not())
                .ToOpenRanges<TOpenRange, TOpenRanges>()
                .All(x => x.Empty);

        public static bool IntersectsWith(TOpenRange first, TOpenRange second) =>
            first.ToInequationFromOpen().And(second.ToInequationFromOpen())
                .ToOpenRanges<TOpenRange, TOpenRanges>()
                .Any(x => !x.Empty);

        public static TOpenRange Intersect(TOpenRange first, TOpenRange second) =>
            first.ToInequationFromOpen().And(second.ToInequationFromOpen())
                .ToOpenRanges<TOpenRange, TOpenRanges>()
                .Single();

        public static bool IsCoveredBy(TOpenRange first, TOpenRange second) =>
            Covers(second, first);

        public static TOpenRange Span(TOpenRange first, TOpenRange second) =>
            first.ToInequationFromOpen().Or(second.ToInequationFromOpen()).Closure()
                .ToOpenRanges<TOpenRange, TOpenRanges>()
                .Single();
    }
}