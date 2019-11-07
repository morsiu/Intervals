namespace Mors.Intervals.Generation.Tests
{
    internal readonly struct PairOfClosedIntervalAndPoint
    {
        private readonly ClosedInterval _first;
        private readonly int _second;

        public PairOfClosedIntervalAndPoint(ClosedInterval first, int second)
        {
            _first = first;
            _second = second;
        }

        public override string ToString() => $"({_first}, {_second})";
    }

}