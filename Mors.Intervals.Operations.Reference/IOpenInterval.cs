using System;

namespace Mors.Intervals.Operations.Reference
{
    public interface IOpenInterval<out TPoint>
        where TPoint : IComparable<TPoint>
    {
        TPoint Start { get; }

        TPoint End { get; }

        bool OpenStart { get; }

        bool OpenEnd { get; }
    }
}