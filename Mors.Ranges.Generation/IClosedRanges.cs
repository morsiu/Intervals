namespace Mors.Ranges.Generation
{
    public interface IClosedRanges<TPoint, TRange>
    {
        TRange Empty();
        TRange Range(TPoint start, TPoint end);
    }
}
