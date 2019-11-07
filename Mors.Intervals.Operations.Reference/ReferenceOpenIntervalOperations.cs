namespace Mors.Intervals.Operations.Reference
{
    public static class ReferenceOpenIntervalOperations<
        TOpenInterval,
        TOpenIntervalUnion,
        TOpenIntervals,
        TOpenIntervalUnions>
        where TOpenInterval : IOpenInterval<int>, IEmptyInterval
        where TOpenIntervals : struct, IOpenIntervals<int, TOpenInterval>, IEmptyIntervals<TOpenInterval>
        where TOpenIntervalUnions : struct, IIntervalUnions<TOpenInterval, TOpenIntervalUnion>
    {
        public static object Contains(in TOpenInterval interval, int point) =>
            ReferenceOperation.Contains(interval.ToInequation(), point);

        public static bool Covers(TOpenInterval first, TOpenInterval second) =>
            ReferenceOperation.Covers(first.ToInequation(), second.ToInequation());

        public static bool IntersectsWith(TOpenInterval first, TOpenInterval second) =>
            ReferenceOperation.IntersectsWith(first.ToInequation(), second.ToInequation());

        public static TOpenInterval Intersect(TOpenInterval first, TOpenInterval second) =>
            ReferenceOperation.Intersect(first.ToInequation(), second.ToInequation())
                .ToOpenInterval<TOpenInterval, TOpenIntervals>();

        public static bool IsCoveredBy(TOpenInterval first, TOpenInterval second) =>
            Covers(second, first);

        public static TOpenInterval Span(TOpenInterval first, TOpenInterval second) =>
            ReferenceOperation.Span(first.ToInequation(), second.ToInequation())
                .ToOpenInterval<TOpenInterval, TOpenIntervals>();

        public static TOpenIntervalUnion Subtract(TOpenInterval first, TOpenInterval second) =>
            ReferenceOperation.Subtract(first.ToInequation(), second.ToInequation())
                .ToOpenIntervalUnion<TOpenInterval, TOpenIntervalUnion, TOpenIntervals, TOpenIntervalUnions>();

        public static TOpenIntervalUnion Union(TOpenInterval first, TOpenInterval second) =>
            ReferenceOperation.Union(first.ToInequation(), second.ToInequation())
                .ToOpenIntervalUnion<TOpenInterval, TOpenIntervalUnion, TOpenIntervals, TOpenIntervalUnions>();
    }
}