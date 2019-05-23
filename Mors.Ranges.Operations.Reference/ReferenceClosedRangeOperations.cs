using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    public static class ReferenceClosedRangeOperations<TClosedRange, TClosedRanges>
        where TClosedRange : IRange<int>, IEmptyRange
        where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
    {
        public static object Contains(in TClosedRange range, int point) =>
            new ContainsOperation(PointSequence(range), point).Result();

        public static bool Covers(in TClosedRange first, in TClosedRange second) =>
            new CoversOperation(PointSequence(first), PointSequence(second)).Result();

        public static bool IntersectsWith(TClosedRange first, TClosedRange second) =>
            new IntersectsWithOperation(PointSequence(first), PointSequence(second)).Result();

        public static TClosedRange Intersect(TClosedRange first, TClosedRange second) =>
            AtMostOneClosedRange(new IntersectOperation(PointSequence(first), PointSequence(second)));

        public static bool IsCoveredBy(TClosedRange first, TClosedRange second) =>
            new IsCoveredByOperation(PointSequence(first), PointSequence(second)).Result();

        public static TClosedRange Span(TClosedRange first, TClosedRange second) =>
            AtMostOneClosedRange(new SpanOperation(PointSequence(first), PointSequence(second)));

        private static TClosedRange AtMostOneClosedRange(IPointSequence pointSequence) =>
            pointSequence.AtMostOneClosedRange<TClosedRange, TClosedRanges>();

        private static IPointSequence PointSequence(in TClosedRange range) =>
            new PointSequenceOfClosedRange<TClosedRange>(range).Value();
    }
}
