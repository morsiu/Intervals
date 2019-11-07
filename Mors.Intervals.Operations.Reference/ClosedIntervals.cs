using System;

namespace Mors.Intervals.Operations.Reference
{
    internal readonly struct ClosedIntervals<T> : Inequations.IClosedIntervals<T, ClosedInterval<T>>
        where T : IComparable<T>
    {
        public ClosedInterval<T> NonEmpty(T start, T end) => new ClosedInterval<T>(start, end);

        public ClosedInterval<T> Empty() => new ClosedInterval<T>();
    }
}