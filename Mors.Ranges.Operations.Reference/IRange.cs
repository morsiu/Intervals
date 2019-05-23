using System;

namespace Mors.Ranges.Operations.Reference
{
    public interface IRange<out TPoint>
        where TPoint : IComparable<TPoint>
    {
        TPoint Start { get; }
        
        TPoint End { get; }
    }
}