namespace Mors.Ranges.Generation
{
    public interface IClosedRanges<in TPoint, out TRange>
    {
        TRange Empty();
        TRange Range(TPoint start, TPoint end);
    }
}
