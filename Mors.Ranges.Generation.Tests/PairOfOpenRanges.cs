namespace Mors.Ranges.Generation.Tests
{
    internal readonly struct PairOfOpenRanges
    {
        private readonly OpenRange _first;
        private readonly OpenRange _second;

        public PairOfOpenRanges(OpenRange first, OpenRange second)
        {
            _first = first;
            _second = second;
        }

        public override string ToString() => $"({_first}, {_second})";
    }

}
