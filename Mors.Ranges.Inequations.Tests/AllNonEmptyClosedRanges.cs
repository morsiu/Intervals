using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Inequations.Tests
{
    internal sealed class AllNonEmptyClosedRanges : IEnumerable<ClosedRange>
    {
        public IEnumerator<ClosedRange> GetEnumerator() =>
            new AllNonEmptyClosedRanges<Point, ClosedRange, ClosedRanges>(
                    new Point(1), new Point(3))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}