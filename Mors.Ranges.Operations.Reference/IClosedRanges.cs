using System;

namespace Mors.Ranges.Operations.Reference
{
    public interface IClosedRanges<in TPoint, out TRange>
        where TPoint : IComparable<TPoint>
        where TRange : IClosedRange<TPoint>
    {
        TRange Range(TPoint start, TPoint end);
    }
}