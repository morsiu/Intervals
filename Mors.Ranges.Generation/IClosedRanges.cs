namespace Mors.Ranges.Generation
{
    public interface IClosedRanges<TRange, out TRangePair>
    {
        TRange Empty();
        TRangePair Pair(TRange first, TRange second);
        TRange Range(int start, int end);
    }
}
