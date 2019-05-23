using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    internal readonly struct ClosedRanges
        : IClosedRanges<int, ClosedRange>,
        Reference.IClosedRanges<int, ClosedRange>,
        IEmptyRanges<ClosedRange>,
        Reference.IEmptyRanges<ClosedRange>,
        IClosedRanges<ClosedRange>
    {
        public ClosedRange Empty() => new ClosedRange();

        public ClosedRange Range(int start, int end) => new ClosedRange(start, end);

        ClosedRange IEmptyRanges<ClosedRange>.Empty()
        {
            return new ClosedRange();
        }

        ClosedRange IClosedRanges<int, ClosedRange>.Range(int start, int end)
        {
            return new ClosedRange(start, end);
        }
    }
}
