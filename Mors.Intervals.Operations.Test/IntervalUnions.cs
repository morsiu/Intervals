using System.Collections.Immutable;

namespace Mors.Intervals.Operations.Test
{
    public readonly struct IntervalUnions<TInterval>
        : IIntervalUnions<TInterval, IntervalUnion<TInterval>>,
        Reference.IIntervalUnions<TInterval, IntervalUnion<TInterval>>
    {
        public IntervalUnion<TInterval> Empty() =>
            IntervalUnion<TInterval>.Empty();

        public IntervalUnion<TInterval> NonEmpty(in TInterval interval) =>
            new IntervalUnion<TInterval>(ImmutableArray.Create(interval));

        public IntervalUnion<TInterval> NonEmpty(in TInterval first, in TInterval second) =>
            new IntervalUnion<TInterval>(ImmutableArray.Create(first, second));
    }
}