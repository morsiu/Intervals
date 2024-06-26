﻿namespace Mors.Intervals.Operations.Reference
{
    public static class ReferenceClosedIntervalUnionOperations<
        TClosedInterval,
        TClosedIntervalUnion,
        TClosedIntervals,
        TClosedIntervalUnions>
        where TClosedInterval : IClosedInterval<int>, IEmptyInterval
        where TClosedIntervals: struct, IClosedIntervals<int, TClosedInterval>, IEmptyIntervals<TClosedInterval>
        where TClosedIntervalUnion : IClosedIntervalUnion<int, TClosedInterval>
        where TClosedIntervalUnions : struct, IIntervalUnions<TClosedInterval, TClosedIntervalUnion>
    {
        public static TClosedIntervalUnion Intersect(TClosedIntervalUnion first, TClosedIntervalUnion second) =>
            ReferenceOperation.Intersect(
                    first.ToInequation<TClosedIntervalUnion, TClosedInterval>(),
                    second.ToInequation<TClosedIntervalUnion, TClosedInterval>())
                .ToClosedIntervalUnion<TClosedInterval, TClosedIntervalUnion, TClosedIntervals, TClosedIntervalUnions>();

        public static TClosedIntervalUnion Subtract(TClosedIntervalUnion first, TClosedIntervalUnion second) =>
            ReferenceOperation.Subtract(
                    first.ToInequation<TClosedIntervalUnion, TClosedInterval>(),
                    second.ToInequation<TClosedIntervalUnion, TClosedInterval>())
                .ToClosedIntervalUnion<TClosedInterval, TClosedIntervalUnion, TClosedIntervals, TClosedIntervalUnions>();

        public static TClosedIntervalUnion Union(TClosedIntervalUnion first, TClosedIntervalUnion second) =>
            ReferenceOperation.Union(
                    first.ToInequation<TClosedIntervalUnion, TClosedInterval>(),
                    second.ToInequation<TClosedIntervalUnion, TClosedInterval>())
                .ToClosedIntervalUnion<TClosedInterval, TClosedIntervalUnion, TClosedIntervals, TClosedIntervalUnions>();
    }
}
