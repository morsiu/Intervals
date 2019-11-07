using System;

namespace Mors.Ranges.Operations.Reference
{
    public interface IOpenRange<out TPoint>
        where TPoint : IComparable<TPoint>
    {
        TPoint Start { get; }

        TPoint End { get; }

        bool OpenStart { get; }

        bool OpenEnd { get; }
    }
}