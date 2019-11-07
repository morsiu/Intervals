namespace Mors.Intervals.Inequations.Tests
{
    public readonly struct OpenIntervals
        : IOpenIntervals<Point, OpenInterval>,
            Generation.IOpenIntervals<Point, OpenInterval>
    {
        public OpenInterval Empty() => OpenInterval.Empty();

        public OpenInterval NonEmpty(Point start, Point end, bool isStartClosed, bool isEndClosed) =>
            (isStartClosed, isEndClosed) switch
            {
                (false, true) => OpenInterval.LeftOpen(start, end),
                (true, true) => OpenInterval.Closed(start, end),
                (true, false) => OpenInterval.RightOpen(start, end),
                _ => OpenInterval.Open(start, end),
            };

        public OpenInterval Interval(Point start, Point end, bool isStartOpen, bool isEndOpen) =>
            NonEmpty(start, end, !isStartOpen, !isEndOpen);
    }
}