namespace Mors.Intervals.Generation.Tests
{
    internal readonly struct PairOfOpenIntervalAndPoint
    {
        private readonly OpenInterval _first;
        private readonly int _second;

        public PairOfOpenIntervalAndPoint(OpenInterval first, int second)
        {
            _first = first;
            _second = second;
        }

        public override string ToString() => $"({_first}, {_second})";
    }

}