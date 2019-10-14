namespace Mors.Ranges.Inequations
{
    public interface IOpenRanges<T, TRange>
    {
        TRange NonEmpty(T start, T end, bool isStartClosed, bool isEndClosed);

        TRange Empty();
    }
}
