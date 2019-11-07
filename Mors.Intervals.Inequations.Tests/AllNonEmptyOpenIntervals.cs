using System.Collections;
using System.Collections.Generic;
using Mors.Intervals.Generation;

namespace Mors.Intervals.Inequations.Tests
{
    internal sealed class AllNonEmptyOpenIntervals : IEnumerable<OpenInterval>
    {
        public IEnumerator<OpenInterval> GetEnumerator() =>
            new AllNonEmptyOpenIntervals<Point, OpenInterval, OpenIntervals>(
                    new Point(1), new Point(3))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}