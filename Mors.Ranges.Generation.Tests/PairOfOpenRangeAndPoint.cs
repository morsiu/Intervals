namespace Mors.Ranges.Generation.Tests
{
    internal readonly struct PairOfOpenRangeAndPoint
    {
        private readonly OpenRange _first;
        private readonly int _second;

        public PairOfOpenRangeAndPoint(OpenRange first, int second)
        {
            _first = first;
            _second = second;
        }

        public override string ToString() => $"({_first}, {_second})";
    }

}