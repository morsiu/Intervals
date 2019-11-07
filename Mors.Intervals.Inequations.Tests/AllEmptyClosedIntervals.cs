using System.Collections;
using System.Collections.Generic;
using Mors.Intervals.Generation;

namespace Mors.Intervals.Inequations.Tests
{
    internal sealed class AllEmptyClosedIntervals : IEnumerable<ClosedInterval>
    {
        public IEnumerator<ClosedInterval> GetEnumerator() =>
            new AllEmptyClosedIntervals<Point, ClosedInterval, ClosedIntervals>()
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}