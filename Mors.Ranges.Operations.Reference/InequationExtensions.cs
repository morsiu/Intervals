using System.Collections.Generic;
using System.Linq;
using Mors.Ranges.Inequations;

namespace Mors.Ranges.Operations.Reference
{
    internal static class InequationExtensions
    {
        public static IEnumerable<TClosedRange> ToClosedRanges<TClosedRange, TClosedRanges>(this Inequation<int> inequation)
            where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
            where TClosedRange : IRange<int>, IEmptyRange =>
            inequation.ToClosedRanges<ClosedRange<int>, Integers, ClosedRanges<int>>()
                .Select(x => x.ToReferenceRange<TClosedRange, TClosedRanges>());

        public static IEnumerable<TOpenRange> ToOpenRanges<TOpenRange, TOpenRanges>(this Inequation<int> inequation)
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
            where TOpenRange : IRange<int>, IOpenRange, IEmptyRange =>
            inequation.ToOpenRanges<OpenRange<int>, Integers, OpenRanges<int>>()
                .Select(x => x.ToReferenceRange<TOpenRange, TOpenRanges>());
    }
}