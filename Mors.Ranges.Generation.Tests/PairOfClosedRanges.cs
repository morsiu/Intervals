using System.Collections.Generic;

namespace Mors.Ranges.Generation.Tests
{
    internal readonly struct PairOfClosedRanges
    {
        private readonly ClosedRange _first;
        private readonly ClosedRange _second;

        public PairOfClosedRanges(ClosedRange first, ClosedRange second)
        {
            _first = first;
            _second = second;
        }

        public IEnumerable<PairOfOpenRanges> ToOpenRangePairs()
        {
            foreach (var first in _first.ToOpenRanges())
            {
                foreach (var second in _second.ToOpenRanges())
                {
                    yield return new PairOfOpenRanges(first, second);
                }
            }
        }

        public override string ToString() => $"({_first}, {_second})";
    }

}
