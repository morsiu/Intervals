using System.Collections.Generic;
using System.Collections.Immutable;

namespace Mors.Intervals.Operations.Test
{
    internal readonly struct ClosedIntervalUnions
        : IIntervalUnions<ClosedInterval, ClosedIntervalUnion>,
        Reference.IIntervalUnions<ClosedInterval, ClosedIntervalUnion>
    {
        public ClosedIntervalUnion Empty() =>
            ClosedIntervalUnion.Empty();

        public ClosedIntervalUnion FromEnumerable(IEnumerable<ClosedInterval> intervals) =>
            new ClosedIntervalUnion(intervals.ToImmutableArray());

        public ClosedIntervalUnion NonEmpty(in ClosedInterval interval) =>
            new ClosedIntervalUnion([interval]);

        public ClosedIntervalUnion NonEmpty(in ClosedInterval first, in ClosedInterval second) =>
            new ClosedIntervalUnion([first, second]);
    }
}