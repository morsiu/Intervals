using System.Collections.Generic;

namespace Mors.Intervals.Generation.Tests
{
    internal readonly struct PairOfClosedIntervals
    {
        private readonly ClosedInterval _first;
        private readonly ClosedInterval _second;

        public PairOfClosedIntervals(ClosedInterval first, ClosedInterval second)
        {
            _first = first;
            _second = second;
        }

        public IEnumerable<PairOfOpenIntervals> ToOpenIntervalPairs()
        {
            foreach (var first in _first.ToOpenIntervals())
            {
                foreach (var second in _second.ToOpenIntervals())
                {
                    yield return new PairOfOpenIntervals(first, second);
                }
            }
        }

        public override string ToString() => $"({_first}, {_second})";
    }

}
