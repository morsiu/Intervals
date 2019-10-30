using System.Collections.Immutable;

namespace Mors.Ranges.Operations
{
    public readonly struct RangeUnions<TRange>
        : IRangeUnions<TRange, RangeUnion<TRange>>,
        Reference.IRangeUnions<TRange, RangeUnion<TRange>>
    {
        public RangeUnion<TRange> Empty() =>
            RangeUnion<TRange>.Empty();

        public RangeUnion<TRange> NonEmpty(in TRange range) =>
            new RangeUnion<TRange>(ImmutableArray.Create(range));

        public RangeUnion<TRange> NonEmpty(in TRange first, in TRange second) =>
            new RangeUnion<TRange>(ImmutableArray.Create(first, second));
    }
}