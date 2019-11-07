namespace Mors.Intervals.Inequations.Tests
{
    public readonly struct ClosedIntervals
        : IClosedIntervals<Point, ClosedInterval>,
            Generation.IClosedIntervals<Point, ClosedInterval>
    {
        public ClosedInterval NonEmpty(Point start, Point end) => ClosedInterval.Closed(start, end);

        public ClosedInterval Interval(Point start, Point end) => ClosedInterval.Closed(start, end);

        public ClosedInterval Empty() => ClosedInterval.Empty();
    }
}