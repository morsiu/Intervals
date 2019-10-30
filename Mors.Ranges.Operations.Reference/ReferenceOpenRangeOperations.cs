namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceOpenRangeOperations<TOpenRange, TOpenRanges>
        where TOpenRange : IOpenRange<int>, IEmptyRange
        where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
    {
        public static object Contains(in TOpenRange range, int point) =>
            ReferenceOperation.Contains(range.ToInequation(), point);

        public static bool Covers(TOpenRange first, TOpenRange second) =>
            ReferenceOperation.Covers(first.ToInequation(), second.ToInequation());

        public static bool IntersectsWith(TOpenRange first, TOpenRange second) =>
            ReferenceOperation.IntersectsWith(first.ToInequation(), second.ToInequation());

        public static TOpenRange Intersect(TOpenRange first, TOpenRange second) =>
            ReferenceOperation.Intersect(first.ToInequation(), second.ToInequation())
                .ToOpenRange<TOpenRange, TOpenRanges>();

        public static bool IsCoveredBy(TOpenRange first, TOpenRange second) =>
            Covers(second, first);

        public static TOpenRange Span(TOpenRange first, TOpenRange second) =>
            ReferenceOperation.Span(first.ToInequation(), second.ToInequation())
                .ToOpenRange<TOpenRange, TOpenRanges>();
    }
}