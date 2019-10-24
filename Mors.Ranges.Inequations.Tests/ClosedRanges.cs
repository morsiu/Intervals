using Mors.Ranges.Generation;

namespace Mors.Ranges.Inequations.Tests
{
    public readonly struct ClosedRanges
        : IClosedRanges<Point, ClosedRange>,
            IClosedRanges<ClosedRange>
    {
        public ClosedRange NonEmpty(Point start, Point end) => ClosedRange.Closed(start, end);

        public ClosedRange Range(int start, int end) => ClosedRange.Closed(start, end);

        public ClosedRange Empty() => ClosedRange.Empty();
    }
}