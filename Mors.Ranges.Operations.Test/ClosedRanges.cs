namespace Mors.Ranges.Operations
{
    internal readonly struct ClosedRanges
        : IClosedRanges<int, ClosedRange>,
        Reference.IClosedRanges<int, ClosedRange>,
        IEmptyRanges<ClosedRange>,
        Reference.IEmptyRanges<ClosedRange>,
        Generation.IClosedRanges<int, ClosedRange>
    {
        public ClosedRange Empty() => new ClosedRange();

        public ClosedRange Range(int start, int end) => new ClosedRange(start, end);
    }
}
