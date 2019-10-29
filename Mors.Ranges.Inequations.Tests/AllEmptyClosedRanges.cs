using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Inequations.Tests
{
    internal sealed class AllEmptyClosedRanges : IEnumerable<ClosedRange>
    {
        public IEnumerator<ClosedRange> GetEnumerator() =>
            new AllEmptyClosedRanges<Point, ClosedRange, ClosedRanges>()
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}