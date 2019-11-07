namespace Mors.Intervals.Generation.Tests
{
    internal struct ClosedIntervals : IClosedIntervals<int, ClosedInterval>
    {
        public ClosedInterval Empty() => new ClosedInterval();
        public ClosedInterval Interval(int start, int end) => new ClosedInterval(start, end);
    }
}
