using System;

namespace Mors.Intervals.Operations
{
    public interface IClosedIntervals<in TPoint, out TInterval>
        where TPoint : IComparable<TPoint>
        where TInterval : IClosedInterval<TPoint>
    {
        TInterval Interval(TPoint start, TPoint end);
    }
}