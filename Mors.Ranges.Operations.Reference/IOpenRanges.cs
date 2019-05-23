using System;

namespace Mors.Ranges.Operations.Reference
{
    public interface IOpenRanges<in TPoint, out TRange>
        where TPoint : IComparable<TPoint>
        where TRange : IRange<TPoint>, IOpenRange
    {
        TRange Range(TPoint start, TPoint end, bool openStart, bool openEnd);
    }
}