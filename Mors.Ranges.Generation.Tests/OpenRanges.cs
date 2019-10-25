namespace Mors.Ranges.Generation.Tests
{
    internal struct OpenRanges : IOpenRanges<int, OpenRange>
    {
        public OpenRange Empty() => new OpenRange();

        public OpenRange Range(int start, int end, bool isStartOpen, bool isEndOpen) =>
            new OpenRange(start, end, isStartOpen, isEndOpen);
    }

}
