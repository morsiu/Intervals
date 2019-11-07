using Mors.Ranges.Inequations;

namespace Mors.Ranges.Operations.Reference
{
    internal static class OpenRangeExtensions
    {
        public static TOpenRange ToReferenceRange<TOpenRange, TOpenRanges>(this OpenRange<int> range)
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
            where TOpenRange : IOpenRange<int>, IEmptyRange
        {
            var ranges = default(TOpenRanges);
            return !range.Empty()
                ? ranges.Range(range.Start(), range.End(), !range.ClosedStart(), !range.ClosedEnd())
                : ranges.Empty();
        }

        public static Inequation<int> ToInequation<TOpenRange>(this TOpenRange range)
            where TOpenRange : IOpenRange<int>, IEmptyRange =>
            Inequation.FromOpenRange<int, OpenRange<int>>(
                range.Empty
                    ? new OpenRange<int>()
                    : new OpenRange<int>(range.Start, range.End, range.OpenStart, range.OpenEnd));
    }
}
