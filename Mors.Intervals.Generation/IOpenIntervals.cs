namespace Mors.Intervals.Generation
{
    public interface IOpenIntervals<in TPoint, out TInterval>
    {
        TInterval Empty();
        TInterval Interval(TPoint start, TPoint end, bool isStartOpen, bool isEndOpen);
    }
}
