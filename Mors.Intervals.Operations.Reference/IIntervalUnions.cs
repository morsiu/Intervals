using System.Collections.Generic;

namespace Mors.Intervals.Operations.Reference
{
    public interface IIntervalUnions<TInterval, out TIntervalUnion> : IEmptyIntervals<TIntervalUnion>
    {
        TIntervalUnion FromEnumerable(IEnumerable<TInterval> intervals);
    }
}