using System;

namespace Mors.Ranges.Operations
{
    public interface IOpenRanges<in TPoint, out TRange>
        where TPoint : IComparable<TPoint>
        where TRange : IOpenRange<TPoint>
    {
        TRange Range(TPoint start, TPoint end, bool openStart, bool openEnd);
    }
}