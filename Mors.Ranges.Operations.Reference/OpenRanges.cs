using System;

namespace Mors.Ranges.Operations.Reference
{
    internal readonly struct OpenRanges<T>
        : Inequations.IOpenRanges<T, OpenRange<T>>
        where T : IComparable<T>
    {
        public OpenRange<T> Empty() => new OpenRange<T>();

        public OpenRange<T> NonEmpty(T start, T end, bool isStartClosed, bool isEndClosed) =>
            new OpenRange<T>(start, end, openStart: !isStartClosed, openEnd: !isEndClosed);
    }
}
