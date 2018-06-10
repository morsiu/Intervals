namespace Mors.Ranges.Generation
{
    public interface IRanges<out TRange>
    {
        TRange Empty();
        TRange NonEmpty(int start, int end, bool isStartOpen, bool isEndOpen);
    }
}