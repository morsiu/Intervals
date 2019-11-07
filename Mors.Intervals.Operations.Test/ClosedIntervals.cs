namespace Mors.Intervals.Operations.Test
{
    internal readonly struct ClosedIntervals
        : IClosedIntervals<int, ClosedInterval>,
        Reference.IClosedIntervals<int, ClosedInterval>,
        IEmptyIntervals<ClosedInterval>,
        Reference.IEmptyIntervals<ClosedInterval>,
        Generation.IClosedIntervals<int, ClosedInterval>
    {
        public ClosedInterval Empty() => new ClosedInterval();

        public ClosedInterval Interval(int start, int end) => new ClosedInterval(start, end);
    }
}
