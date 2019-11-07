using System;

namespace Mors.Intervals.Operations
{
    public interface IClosedInterval<out TPoint>
        where TPoint : IComparable<TPoint>
    {
        TPoint Start { get; }
        
        TPoint End { get; }
    }
}