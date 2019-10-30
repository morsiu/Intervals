namespace Mors.Ranges.Operations
{
    public interface IRangeUnions<TRange, out TRangeUnion> : IEmptyRanges<TRangeUnion>
    {
        TRangeUnion NonEmpty(in TRange range);

        TRangeUnion NonEmpty(in TRange first, in TRange second);
    }
}