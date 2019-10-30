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

        public static TClosedRangeUnion ToClosedRangeUnion<
            TClosedRange,
            TClosedRangeUnion,
            TClosedRanges,
            TClosedRangeUnions>(
            this Inequation<int> inequation)
            where TClosedRange : IClosedRange<int>, IEmptyRange
            where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
            where TClosedRangeUnions : struct, IRangeUnions<TClosedRange, TClosedRangeUnion> =>
            inequation
                .ToClosedRanges<TClosedRange, TClosedRanges>()
                .ToRangeUnion<TClosedRange, TClosedRangeUnion, TClosedRangeUnions>();

        public static TOpenRange ToOpenRange<TOpenRange, TOpenRanges>(
            this Inequation<int> inequation)
            where TOpenRange : IOpenRange<int>, IEmptyRange
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange> =>
            inequation
                .ToOpenRanges<TOpenRange, TOpenRanges>()
                .ToRange<TOpenRange, TOpenRanges>();

        public static TOpenRangeUnion ToOpenRangeUnion<
            TOpenRange,
            TOpenRangeUnion,
            TOpenRanges,
            TOpenRangeUnions>(
            this Inequation<int> inequation)
            where TOpenRange : IOpenRange<int>, IEmptyRange
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
            where TOpenRangeUnions : struct, IRangeUnions<TOpenRange, TOpenRangeUnion> =>
            inequation.ToOpenRanges<TOpenRange, TOpenRanges>()
                .ToRangeUnion<TOpenRange, TOpenRangeUnion, TOpenRangeUnions>();

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

        private static TRangeUnion ToRangeUnion<TRange, TRangeUnion, TRangeUnions>(
            this IEnumerable<TRange> ranges)
            where TRangeUnions : struct, IRangeUnions<TRange, TRangeUnion>
        {
            using var enumerator = ranges.GetEnumerator();
            if (!enumerator.MoveNext()) return default(TRangeUnions).Empty();
            var first = enumerator.Current;
            if (!enumerator.MoveNext()) return default(TRangeUnions).NonEmpty(first);
            var second = enumerator.Current;
            if (!enumerator.MoveNext()) return default(TRangeUnions).NonEmpty(first, second);
            throw new Exception("Conversion of more than two ranges to a range union is not supported.");
        }
    }
}