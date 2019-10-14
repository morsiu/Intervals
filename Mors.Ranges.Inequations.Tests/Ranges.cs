namespace Mors.Ranges.Inequations.Tests
{
    public readonly struct Ranges : IOpenRanges<Point, Range>
    {
        public Range Empty() => Range.Empty();

        public Range NonEmpty(Point start, Point end, bool isStartClosed, bool isEndClosed) =>
            (isStartClosed, isEndClosed) switch
            {
                (false, false) => Range.Open(start, end),
                (false, true) => Range.LeftOpen(start, end),
                (true, true) => Range.Closed(start, end),
                (true, false) => Range.RightOpen(start, end)
            };
    }
}