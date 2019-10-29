using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Inequations.Tests
{
    internal sealed class AllNonEmptyOpenRanges : IEnumerable<OpenRange>
    {
        public IEnumerator<OpenRange> GetEnumerator() =>
            new AllNonEmptyOpenRanges<Point, OpenRange, OpenRanges>(
                    new Point(1), new Point(3))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}