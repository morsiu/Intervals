namespace Mors.Ranges.Inequations.Tests
{
    public readonly struct ClosedRanges : IClosedRanges<Point, ClosedRange>
    {
        public ClosedRange NonEmpty(Point start, Point end) => ClosedRange.Closed(start, end);

        ClosedRange IClosedRanges<Point, ClosedRange>.Empty() => ClosedRange.Empty();
    }
}