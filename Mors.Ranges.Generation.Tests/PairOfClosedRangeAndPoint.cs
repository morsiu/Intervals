namespace Mors.Ranges.Generation.Tests
{
    internal readonly struct PairOfClosedRangeAndPoint
    {
        private readonly ClosedRange _first;
        private readonly int _second;

        public PairOfClosedRangeAndPoint(ClosedRange first, int second)
        {
            _first = first;
            _second = second;
        }

        public override string ToString() => $"({_first}, {_second})";
    }

}