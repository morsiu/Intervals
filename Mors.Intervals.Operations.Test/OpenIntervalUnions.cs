using System.Collections.Generic;
using System.Collections.Immutable;

namespace Mors.Intervals.Operations.Test
{
    public readonly struct OpenIntervalUnions
        : IIntervalUnions<OpenInterval, OpenIntervalUnion>,
        Reference.IIntervalUnions<OpenInterval, OpenIntervalUnion>
    {
        public OpenIntervalUnion Empty() =>
            OpenIntervalUnion.Empty();

        public OpenIntervalUnion FromEnumerable(IEnumerable<OpenInterval> intervals) =>
            new OpenIntervalUnion(intervals.ToImmutableArray());

        public OpenIntervalUnion NonEmpty(in OpenInterval interval) =>
            new OpenIntervalUnion([interval]);

        public OpenIntervalUnion NonEmpty(in OpenInterval first, in OpenInterval second) =>
            new OpenIntervalUnion([first, second]);
    }
}