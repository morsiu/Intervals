using System.Collections.Immutable;

namespace Mors.Intervals.Operations.Test
{
    internal struct ClosedIntervalUnionBuilder
        : IIntervalUnionBuilder<ClosedIntervalUnion, ClosedInterval>
    {
        private ImmutableArray<ClosedInterval>.Builder? _intervals;

        private ImmutableArray<ClosedInterval>.Builder Intervals => _intervals ??= ImmutableArray.CreateBuilder<ClosedInterval>();

        public void Append(in ClosedInterval interval) => Intervals.Add(interval);

        public ClosedIntervalUnion Build() => new ClosedIntervalUnion(Intervals.ToImmutable());
    }
}
