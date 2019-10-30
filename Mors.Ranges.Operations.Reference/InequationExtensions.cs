using System;
using System.Collections.Generic;
using System.Linq;
using Mors.Ranges.Inequations;

namespace Mors.Ranges.Operations.Reference
{
    internal static class InequationExtensions
    {
        public static TClosedRange ToClosedRange<TClosedRange, TClosedRanges>(
            this Inequation<int> inequation)
            where TClosedRange : IClosedRange<int>, IEmptyRange
            where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange> =>
            inequation
                .ToClosedRanges<TClosedRange, TClosedRanges>()
                .ToRange<TClosedRange, TClosedRanges>();

        public static TOpenRange ToOpenRange<TOpenRange, TOpenRanges>(
            this Inequation<int> inequation)
            where TOpenRange : IOpenRange<int>, IEmptyRange
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange> =>
            inequation
                .ToOpenRanges<TOpenRange, TOpenRanges>()
                .ToRange<TOpenRange, TOpenRanges>();

        private static IEnumerable<TClosedRange> ToClosedRanges<TClosedRange, TClosedRanges>(
            this Inequation<int> inequation)
            where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
            where TClosedRange : IClosedRange<int>, IEmptyRange =>
            inequation.ToClosedRanges<ClosedRange<int>, Integers, ClosedRanges<int>>()
                .Select(x => x.ToReferenceRange<TClosedRange, TClosedRanges>());

        private static IEnumerable<TOpenRange> ToOpenRanges<TOpenRange, TOpenRanges>(this Inequation<int> inequation)
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
            where TOpenRange : IOpenRange<int>, IEmptyRange =>
            inequation.ToOpenRanges<OpenRange<int>, Integers, OpenRanges<int>>()
                .Select(x => x.ToReferenceRange<TOpenRange, TOpenRanges>());

        private static TRange ToRange<TRange, TRanges>(
            this IEnumerable<TRange> ranges)
            where TRanges : struct, IEmptyRanges<TRange>
        {
            using var enumerator = ranges.GetEnumerator();
            if (!enumerator.MoveNext()) return default(TRanges).Empty();
            var first = enumerator.Current;
            if (!enumerator.MoveNext()) return first;
            throw new Exception("Expected at most one range.");
        }
    }
}