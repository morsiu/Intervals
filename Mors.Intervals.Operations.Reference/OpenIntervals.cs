using System;

namespace Mors.Intervals.Operations.Reference
{
    internal readonly struct OpenIntervals<T>
        : Inequations.IOpenIntervals<T, OpenInterval<T>>
        where T : IComparable<T>
    {
        public OpenInterval<T> Empty() => new OpenInterval<T>();

        public OpenInterval<T> NonEmpty(T start, T end, bool isStartClosed, bool isEndClosed) =>
            new OpenInterval<T>(start, end, openStart: !isStartClosed, openEnd: !isEndClosed);
    }
}
