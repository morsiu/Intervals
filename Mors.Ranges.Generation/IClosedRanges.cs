namespace Mors.Ranges.Generation
{
    public interface IClosedRanges<TRange>
    {
        TRange Empty();
        TRange Range(int start, int end);
    }
}
