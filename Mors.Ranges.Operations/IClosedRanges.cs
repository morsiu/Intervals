using System;

namespace Mors.Ranges.Operations
{
    public interface IClosedRanges<in TPoint, out TRange>
        where TPoint : IComparable<TPoint>
        where TRange : IRange<TPoint>
    {
        TRange Range(TPoint start, TPoint end);
    }
}