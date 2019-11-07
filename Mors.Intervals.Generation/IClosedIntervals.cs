namespace Mors.Intervals.Generation
{
    public interface IClosedIntervals<in TPoint, out TInterval>
    {
        TInterval Empty();
        TInterval Interval(TPoint start, TPoint end);
    }
}
