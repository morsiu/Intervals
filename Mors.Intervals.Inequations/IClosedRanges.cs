namespace Mors.Intervals.Inequations
{
    public interface IClosedIntervals<in T, out TInterval>
    {
        TInterval NonEmpty(T start, T end);

        TInterval Empty();
    }
}
