namespace Mors.Intervals.Operations.Reference
{
    public static class ReferenceClosedIntervalOperations<
        TClosedInterval,
        TClosedIntervalUnion,
        TClosedIntervals,
        TClosedIntervalUnions>
        where TClosedInterval : IClosedInterval<int>, IEmptyInterval
        where TClosedIntervals : struct, IClosedIntervals<int, TClosedInterval>, IEmptyIntervals<TClosedInterval>
        where TClosedIntervalUnions : struct, IIntervalUnions<TClosedInterval, TClosedIntervalUnion>
    {
        public static object Contains(in TClosedInterval interval, int point) =>
            ReferenceOperation.Contains(interval.ToInequation(), point);

        public static bool Covers(in TClosedInterval first, in TClosedInterval second) =>
            ReferenceOperation.Covers(first.ToInequation(), second.ToInequation());

        public static bool IntersectsWith(TClosedInterval first, TClosedInterval second) =>
            ReferenceOperation.IntersectsWith(first.ToInequation(), second.ToInequation());

        public static TClosedInterval Intersect(TClosedInterval first, TClosedInterval second) =>
            ReferenceOperation.Intersect(first.ToInequation(), second.ToInequation())
                .ToClosedInterval<TClosedInterval, TClosedIntervals>();

        public static bool IsCoveredBy(TClosedInterval first, TClosedInterval second) =>
            Covers(second, first);

        public static TClosedInterval Span(TClosedInterval first, TClosedInterval second) =>
            ReferenceOperation.Span(first.ToInequation(), second.ToInequation())
                .ToClosedInterval<TClosedInterval, TClosedIntervals>();

        public static TClosedIntervalUnion Subtract(TClosedInterval first, TClosedInterval second) =>
            ReferenceOperation.Subtract(first.ToInequation(), second.ToInequation())
                .ToClosedIntervalUnion<TClosedInterval, TClosedIntervalUnion, TClosedIntervals, TClosedIntervalUnions>();

        public static TClosedIntervalUnion Union(TClosedInterval first, TClosedInterval second) =>
            ReferenceOperation.Union(first.ToInequation(), second.ToInequation())
                .ToClosedIntervalUnion<TClosedInterval, TClosedIntervalUnion, TClosedIntervals, TClosedIntervalUnions>();
    }
}
