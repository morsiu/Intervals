namespace Mors.Ranges.Inequations.Tests
{
    public readonly struct OpenRanges
        : IOpenRanges<Point, OpenRange>,
            Generation.IOpenRanges<Point, OpenRange>
    {
        public OpenRange Empty() => OpenRange.Empty();

        public OpenRange NonEmpty(Point start, Point end, bool isStartClosed, bool isEndClosed) =>
            (isStartClosed, isEndClosed) switch
            {
                (false, true) => OpenRange.LeftOpen(start, end),
                (true, true) => OpenRange.Closed(start, end),
                (true, false) => OpenRange.RightOpen(start, end),
                _ => OpenRange.Open(start, end),
            };

        public OpenRange Range(Point start, Point end, bool isStartOpen, bool isEndOpen) =>
            NonEmpty(start, end, !isStartOpen, !isEndOpen);
    }
}