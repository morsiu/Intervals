using System;
using System.Collections.Generic;
using System.Linq;
using Mors.Intervals.Inequations;

namespace Mors.Intervals.Operations.Reference
{
    internal static class InequationExtensions
    {
        public static TClosedInterval ToClosedInterval<TClosedInterval, TClosedIntervals>(
            this Inequation<int> inequation)
            where TClosedInterval : IClosedInterval<int>, IEmptyInterval
            where TClosedIntervals : struct, IClosedIntervals<int, TClosedInterval>, IEmptyIntervals<TClosedInterval> =>
            inequation
                .ToClosedIntervals<TClosedInterval, TClosedIntervals>()
                .ToInterval<TClosedInterval, TClosedIntervals>();

        public static TClosedIntervalUnion ToClosedIntervalUnion<
            TClosedInterval,
            TClosedIntervalUnion,
            TClosedIntervals,
            TClosedIntervalUnions>(
            this Inequation<int> inequation)
            where TClosedInterval : IClosedInterval<int>, IEmptyInterval
            where TClosedIntervals : struct, IClosedIntervals<int, TClosedInterval>, IEmptyIntervals<TClosedInterval>
            where TClosedIntervalUnions : struct, IIntervalUnions<TClosedInterval, TClosedIntervalUnion> =>
            inequation
                .ToClosedIntervals<TClosedInterval, TClosedIntervals>()
                .ToIntervalUnion<TClosedInterval, TClosedIntervalUnion, TClosedIntervalUnions>();

        public static TOpenInterval ToOpenInterval<TOpenInterval, TOpenIntervals>(
            this Inequation<int> inequation)
            where TOpenInterval : IOpenInterval<int>, IEmptyInterval
            where TOpenIntervals : struct, IOpenIntervals<int, TOpenInterval>, IEmptyIntervals<TOpenInterval> =>
            inequation
                .ToOpenIntervals<TOpenInterval, TOpenIntervals>()
                .ToInterval<TOpenInterval, TOpenIntervals>();

        public static TOpenIntervalUnion ToOpenIntervalUnion<
            TOpenInterval,
            TOpenIntervalUnion,
            TOpenIntervals,
            TOpenIntervalUnions>(
            this Inequation<int> inequation)
            where TOpenInterval : IOpenInterval<int>, IEmptyInterval
            where TOpenIntervals : struct, IOpenIntervals<int, TOpenInterval>, IEmptyIntervals<TOpenInterval>
            where TOpenIntervalUnions : struct, IIntervalUnions<TOpenInterval, TOpenIntervalUnion> =>
            inequation.ToOpenIntervals<TOpenInterval, TOpenIntervals>()
                .ToIntervalUnion<TOpenInterval, TOpenIntervalUnion, TOpenIntervalUnions>();

        private static IEnumerable<TClosedInterval> ToClosedIntervals<TClosedInterval, TClosedIntervals>(
            this Inequation<int> inequation)
            where TClosedIntervals : struct, IClosedIntervals<int, TClosedInterval>, IEmptyIntervals<TClosedInterval>
            where TClosedInterval : IClosedInterval<int>, IEmptyInterval =>
            inequation.ToClosedIntervals<ClosedInterval<int>, Integers, ClosedIntervals<int>>()
                .Select(x => x.ToReferenceInterval<TClosedInterval, TClosedIntervals>());

        private static IEnumerable<TOpenInterval> ToOpenIntervals<TOpenInterval, TOpenIntervals>(this Inequation<int> inequation)
            where TOpenIntervals : struct, IOpenIntervals<int, TOpenInterval>, IEmptyIntervals<TOpenInterval>
            where TOpenInterval : IOpenInterval<int>, IEmptyInterval =>
            inequation.ToOpenIntervals<OpenInterval<int>, Integers, OpenIntervals<int>>()
                .Select(x => x.ToReferenceInterval<TOpenInterval, TOpenIntervals>());

        private static TInterval ToInterval<TInterval, TIntervals>(
            this IEnumerable<TInterval> intervals)
            where TIntervals : struct, IEmptyIntervals<TInterval>
        {
            using var enumerator = intervals.GetEnumerator();
            if (!enumerator.MoveNext()) return default(TIntervals).Empty();
            var first = enumerator.Current;
            if (!enumerator.MoveNext()) return first;
            throw new Exception("Expected at most one interval.");
        }

        private static TIntervalUnion ToIntervalUnion<TInterval, TIntervalUnion, TIntervalUnions>(
            this IEnumerable<TInterval> intervals)
            where TIntervalUnions : struct, IIntervalUnions<TInterval, TIntervalUnion>
        {
            using var enumerator = intervals.GetEnumerator();
            if (!enumerator.MoveNext()) return default(TIntervalUnions).Empty();
            var first = enumerator.Current;
            if (!enumerator.MoveNext()) return default(TIntervalUnions).NonEmpty(first);
            var second = enumerator.Current;
            if (!enumerator.MoveNext()) return default(TIntervalUnions).NonEmpty(first, second);
            throw new Exception("Conversion of more than two intervals to a interval union is not supported.");
        }
    }
}