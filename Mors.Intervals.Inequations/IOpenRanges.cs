namespace Mors.Intervals.Inequations
{
    public interface IOpenIntervals<in T, out TInterval>
    {
        TInterval NonEmpty(T start, T end, bool isStartClosed, bool isEndClosed);

        TInterval Empty();
    }
}
