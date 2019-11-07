namespace Mors.Intervals.Generation.Tests
{
    internal readonly struct Pairs
        : IPairs<ClosedInterval, ClosedInterval, PairOfClosedIntervals>,
        IPairs<OpenInterval, OpenInterval, PairOfOpenIntervals>,
        IPairs<ClosedInterval, int, PairOfClosedIntervalAndPoint>,
        IPairs<OpenInterval, int, PairOfOpenIntervalAndPoint>
    {
        public PairOfClosedIntervals Pair(ClosedInterval first, ClosedInterval second) => new PairOfClosedIntervals(first, second);

        public PairOfOpenIntervals Pair(OpenInterval first, OpenInterval second) => new PairOfOpenIntervals(first, second);

        public PairOfClosedIntervalAndPoint Pair(ClosedInterval first, int second) => new PairOfClosedIntervalAndPoint(first, second);

        public PairOfOpenIntervalAndPoint Pair(OpenInterval first, int second) => new PairOfOpenIntervalAndPoint(first, second);
    }
}
