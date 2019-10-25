namespace Mors.Ranges.Operations
{
    internal readonly struct OpenRanges
        : IOpenRanges<int, OpenRange>,
        Reference.IOpenRanges<int, OpenRange>,
        IEmptyRanges<OpenRange>,
        Reference.IEmptyRanges<OpenRange>,
        Generation.IOpenRanges<int, OpenRange>
    {
        public OpenRange Empty() => new OpenRange();

        public OpenRange Range(int start, int end, bool isStartOpen, bool isEndOpen) =>
            new OpenRange(start, end, isStartOpen, isEndOpen);
    }
}
