namespace Mors.Ranges.Generation
{
    public interface IOpenRanges<TRange>
    {
        TRange Empty();
        TRange Range(int start, int end, bool isStartOpen, bool isEndOpen);
    }
}
