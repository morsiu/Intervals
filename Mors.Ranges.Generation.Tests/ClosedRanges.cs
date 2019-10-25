namespace Mors.Ranges.Generation.Tests
{
    internal struct ClosedRanges : IClosedRanges<int, ClosedRange>
    {
        public ClosedRange Empty() => new ClosedRange();
        public ClosedRange Range(int start, int end) => new ClosedRange(start, end);
    }
}
