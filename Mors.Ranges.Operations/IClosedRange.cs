using System;

namespace Mors.Ranges.Operations
{
    public interface IClosedRange<TPoint>
        where TPoint : IComparable<TPoint>
    {
        TPoint Start { get; }
        
        TPoint End { get; }
    }
}