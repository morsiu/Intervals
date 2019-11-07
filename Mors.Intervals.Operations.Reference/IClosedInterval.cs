using System;

namespace Mors.Intervals.Operations.Reference
{
    public interface IClosedInterval<out TPoint>
        where TPoint : IComparable<TPoint>
    {
        TPoint Start { get; }
        
        TPoint End { get; }
    }
}