using System;

namespace Mors.Ranges.Operations
{
    public interface IOpenRange<TPoint>
        where TPoint : IComparable<TPoint>
    {
        TPoint Start { get; }
        
        TPoint End { get; }

        bool OpenStart { get; }

        bool OpenEnd { get; }
    }
}