using Mors.Ranges.Generation;

namespace Mors.Ranges.Operations
{
    internal readonly struct OpenRanges
        : IOpenRanges<int, OpenRange>,
        Reference.IOpenRanges<int, OpenRange>,
        IEmptyRanges<OpenRange>,
        Reference.IEmptyRanges<OpenRange>,
        IOpenRanges<OpenRange>
    {
        public OpenRange Empty() => new OpenRange();

        public OpenRange Range(int start, int end, bool isStartOpen, bool isEndOpen) =>
            new OpenRange(start, end, isStartOpen, isEndOpen);

        OpenRange IEmptyRanges<OpenRange>.Empty()
        {
            return new OpenRange();
        }

        OpenRange IOpenRanges<int, OpenRange>.Range(int start, int end, bool openStart, bool openEnd)
        {
            return new OpenRange(start, end, openStart, openEnd);
        }
    }
}
