using System.Collections;
using System.Collections.Generic;
using Mors.Intervals.Generation;

namespace Mors.Intervals.Inequations.Tests
{
    internal sealed class AllNonEmptyClosedIntervals : IEnumerable<ClosedInterval>
    {
        public IEnumerator<ClosedInterval> GetEnumerator() =>
            new AllNonEmptyClosedIntervals<Point, ClosedInterval, ClosedIntervals>(
                    new Point(1), new Point(3))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}