namespace Mors.Ranges.Generation
{
    public interface IOpenRanges<TPoint, TRange>
    {
        TRange Empty();
        TRange Range(TPoint start, TPoint end, bool isStartOpen, bool isEndOpen);
    }
}
