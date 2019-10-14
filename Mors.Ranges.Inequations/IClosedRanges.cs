namespace Mors.Ranges.Inequations
{
    public interface IClosedRanges<T, TRange>
    {
        TRange NonEmpty(T start, T end);

        TRange Empty();
    }
}
