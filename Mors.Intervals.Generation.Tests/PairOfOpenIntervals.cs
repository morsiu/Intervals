namespace Mors.Intervals.Generation.Tests
{
    internal readonly struct PairOfOpenIntervals
    {
        private readonly OpenInterval _first;
        private readonly OpenInterval _second;

        public PairOfOpenIntervals(OpenInterval first, OpenInterval second)
        {
            _first = first;
            _second = second;
        }

        public override string ToString() => $"({_first}, {_second})";
    }

}
