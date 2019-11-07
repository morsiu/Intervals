namespace Mors.Ranges.Inequations
{
    public interface IOpenRanges<in T, out TRange>
    {
        TRange NonEmpty(T start, T end, bool isStartClosed, bool isEndClosed);

        TRange Empty();
    }
}
