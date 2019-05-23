using System;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal static class RangeExtensions
    {
        public static TOpenRange ToOpenRange<TOpenRange, TOpenRanges>(this Range? range)
            where TOpenRange : IRange<int>, IOpenRange
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
        {
            var ranges = default(TOpenRanges);
            return range is Range x
                ? ranges.Range(x.Start, x.End, x.HasOpenStart, x.HasOpenEnd)
                : ranges.Empty();
        }

        public static TClosedRange ToClosedRange<TClosedRange, TClosedRanges>(this Range? range)
            where TClosedRange : IRange<int>
            where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
        {
            var ranges = default(TClosedRanges);
            return range is Range x
                ? !x.HasOpenStart && !x.HasOpenEnd
                    ? ranges.Range(x.Start, x.End)
                    : throw new ArgumentException("The range has open ends.", nameof(range))
                : ranges.Empty();
        }
    }
}