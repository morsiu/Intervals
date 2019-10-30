namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceClosedRangeOperations<TClosedRange, TClosedRanges>
        where TClosedRange : IClosedRange<int>, IEmptyRange
        where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
    {
        public static object Contains(in TClosedRange range, int point) =>
            ReferenceOperation.Contains(range.ToInequation(), point);

        public static bool Covers(in TClosedRange first, in TClosedRange second) =>
            ReferenceOperation.Covers(first.ToInequation(), second.ToInequation());

        public static bool IntersectsWith(TClosedRange first, TClosedRange second) =>
            ReferenceOperation.IntersectsWith(first.ToInequation(), second.ToInequation());

        public static TClosedRange Intersect(TClosedRange first, TClosedRange second) =>
            ReferenceOperation.Intersect(first.ToInequation(), second.ToInequation())
                .ToClosedRange<TClosedRange, TClosedRanges>();

        public static bool IsCoveredBy(TClosedRange first, TClosedRange second) =>
            Covers(second, first);

        public static TClosedRange Span(TClosedRange first, TClosedRange second) =>
            ReferenceOperation.Span(first.ToInequation(), second.ToInequation())
                .ToClosedRange<TClosedRange, TClosedRanges>();
    }
}
