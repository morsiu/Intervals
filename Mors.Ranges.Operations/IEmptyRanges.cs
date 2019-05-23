namespace Mors.Ranges.Operations
{
    public interface IEmptyRanges<out TRange>
    {
        TRange Empty();
    }
}