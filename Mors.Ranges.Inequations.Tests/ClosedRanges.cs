namespace Mors.Ranges.Inequations.Tests
{
    public readonly struct ClosedRanges
        : IClosedRanges<Point, ClosedRange>,
            Generation.IClosedRanges<Point, ClosedRange>
    {
        public ClosedRange NonEmpty(Point start, Point end) => ClosedRange.Closed(start, end);

        public ClosedRange Range(Point start, Point end) => ClosedRange.Closed(start, end);

        public ClosedRange Empty() => ClosedRange.Empty();
    }
}