using Mors.Intervals.Generation;

namespace Mors.Intervals.Operations.Test
{
    internal readonly struct Pairs
        : IPairs<ClosedInterval, ClosedInterval, (ClosedInterval, ClosedInterval)>,
        IPairs<OpenInterval, OpenInterval, (OpenInterval, OpenInterval)>,
        IPairs<ClosedInterval, int, (ClosedInterval, int)>,
        IPairs<OpenInterval, int, (OpenInterval, int)>
    {
        public (OpenInterval, OpenInterval) Pair(OpenInterval first, OpenInterval second) => (first, second);

        public (ClosedInterval, ClosedInterval) Pair(ClosedInterval first, ClosedInterval second) => (first, second);

        public (OpenInterval, int) Pair(OpenInterval first, int second) => (first, second);

        public (ClosedInterval, int) Pair(ClosedInterval first, int second) => (first, second);
    }
}
