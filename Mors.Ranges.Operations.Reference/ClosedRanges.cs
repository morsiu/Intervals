using System;

namespace Mors.Ranges.Operations.Reference
{
    internal readonly struct ClosedRanges<T> : Inequations.IClosedRanges<T, ClosedRange<T>>
        where T : IComparable<T>
    {
        public ClosedRange<T> NonEmpty(T start, T end) => new ClosedRange<T>(start, end);

        public ClosedRange<T> Empty() => new ClosedRange<T>();
    }
}