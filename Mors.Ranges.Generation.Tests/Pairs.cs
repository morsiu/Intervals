namespace Mors.Ranges.Generation.Tests
{
    internal readonly struct Pairs
        : IPairs<ClosedRange, ClosedRange, PairOfClosedRanges>,
        IPairs<OpenRange, OpenRange, PairOfOpenRanges>,
        IPairs<ClosedRange, int, PairOfClosedRangeAndPoint>,
        IPairs<OpenRange, int, PairOfOpenRangeAndPoint>
    {
        public PairOfClosedRanges Pair(ClosedRange first, ClosedRange second) => new PairOfClosedRanges(first, second);

        public PairOfOpenRanges Pair(OpenRange first, OpenRange second) => new PairOfOpenRanges(first, second);

        public PairOfClosedRangeAndPoint Pair(ClosedRange first, int second) => new PairOfClosedRangeAndPoint(first, second);

        public PairOfOpenRangeAndPoint Pair(OpenRange first, int second) => new PairOfOpenRangeAndPoint(first, second);
    }
}
