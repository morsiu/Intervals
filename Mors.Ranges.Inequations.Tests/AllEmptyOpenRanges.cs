using System.Collections;
using System.Collections.Generic;
using Mors.Ranges.Generation;

namespace Mors.Ranges.Inequations.Tests
{
    internal sealed class AllEmptyOpenRanges : IEnumerable<OpenRange>
    {
        public IEnumerator<OpenRange> GetEnumerator() =>
            new AllEmptyOpenRanges<Point, OpenRange, OpenRanges>(new Point(1))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}