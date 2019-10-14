using Mors.Ranges.Inequations;

namespace Mors.Ranges.Operations.Reference
{
    internal static class CloseRangeExtensions
    {
        public static TClosedRange ToReferenceRange<TClosedRange, TClosedRanges>(
            this ClosedRange<int> range)
            where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
            where TClosedRange : IRange<int>, IEmptyRange
        {
            var ranges = default(TClosedRanges);
            return !range.Empty()
                ? ranges.Range(range.Start(), range.End())
                : ranges.Empty();
        }

        public static Inequation<int> ToInequationFromClosed<TClosedRange>(this TClosedRange range)
            where TClosedRange : IRange<int>, IEmptyRange =>
            Inequation.FromClosedRange<int, ClosedRange<int>>(
                range.Empty
                    ? new ClosedRange<int>()
                    : new ClosedRange<int>(range.Start, range.End));
    }
}