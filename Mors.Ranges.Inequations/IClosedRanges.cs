namespace Mors.Ranges.Inequations
{
    public interface IClosedRanges<in T, out TRange>
    {
        TRange NonEmpty(T start, T end);

        TRange Empty();
    }
}
