using System.Linq;
using Mors.Ranges.Sequences;

namespace Mors.Ranges.Operations.Reference
{
    internal static class PointSequenceExtensions
    {
        public static TOpenRange AtMostOneOpenRange<TOpenRange, TOpenRanges>(this IPointSequence pointSequence)
            where TOpenRange : IRange<int>, IOpenRange
            where TOpenRanges : struct, IOpenRanges<int, TOpenRange>, IEmptyRanges<TOpenRange>
        {
            return new RangesInPointSequence(pointSequence)
                .Cast<Range?>()
                .DefaultIfEmpty(default)
                .Select(x => x.ToOpenRange<TOpenRange, TOpenRanges>())
                .Single();
        }

        public static TClosedRange AtMostOneClosedRange<TClosedRange, TClosedRanges>(this IPointSequence pointSequence)
            where TClosedRange : IRange<int>
            where TClosedRanges : struct, IClosedRanges<int, TClosedRange>, IEmptyRanges<TClosedRange>
        {
            return new RangesInPointSequence(pointSequence)
                .Cast<Range?>()
                .DefaultIfEmpty(default)
                .Select(x => x.ToClosedRange<TClosedRange, TClosedRanges>())
                .Single(); 
        }
    }
}