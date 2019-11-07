using System;

namespace Mors.Intervals.Operations
{
    public interface IOpenIntervals<in TPoint, out TInterval>
        where TPoint : IComparable<TPoint>
        where TInterval : IOpenInterval<TPoint>
    {
        TInterval Interval(TPoint start, TPoint end, bool openStart, bool openEnd);
    }
}