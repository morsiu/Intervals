using System.Collections;
using System.Collections.Generic;
using Mors.Intervals.Generation;

namespace Mors.Intervals.Inequations.Tests
{
    internal sealed class AllEmptyOpenIntervals : IEnumerable<OpenInterval>
    {
        public IEnumerator<OpenInterval> GetEnumerator() =>
            new AllEmptyOpenIntervals<Point, OpenInterval, OpenIntervals>(new Point(1))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}