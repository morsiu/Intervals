using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceOpenRangeOperations<TOpenRange, TOpenRanges>
        where TOpenRange : IRange<int>, IOpenRange, IEmptyRange
        where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
    {
        public static object Contains(in TOpenRange range, int point) =>
            new ContainsOperation(PointSequence(range), point).Result();

        public static bool Covers(TOpenRange first, TOpenRange second) =>
            new CoversOperation(PointSequence(first), PointSequence(second)).Result();

        public static bool IntersectsWith(TOpenRange first, TOpenRange second) =>
            new IntersectsWithOperation(PointSequence(first), PointSequence(second)).Result();

        public static TOpenRange Intersect(TOpenRange first, TOpenRange second) =>
            new IntersectOperation(PointSequence(first), PointSequence(second))
                .AtMostOneOpenRange<TOpenRange, TOpenRanges>();

        public static bool IsCoveredBy(TOpenRange first, TOpenRange second) =>
            new IsCoveredByOperation(PointSequence(first), PointSequence(second)).Result();

        public static TOpenRange Span(TOpenRange first, TOpenRange second) =>
            new SpanOperation(PointSequence(first), PointSequence(second))
                .AtMostOneOpenRange<TOpenRange, TOpenRanges>();

        private static IPointSequence PointSequence(in TOpenRange range) =>
            new PointSequenceOfOpenRange<TOpenRange>(range).Value();
    }
}
