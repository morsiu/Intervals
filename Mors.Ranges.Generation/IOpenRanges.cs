namespace Mors.Ranges.Generation
{
    public interface IOpenRanges<in TPoint, out TRange>
    {
        TRange Empty();
        TRange Range(TPoint start, TPoint end, bool isStartOpen, bool isEndOpen);
    }
}
