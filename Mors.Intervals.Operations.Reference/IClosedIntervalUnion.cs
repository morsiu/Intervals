using System;
using System.Collections.Generic;

namespace Mors.Intervals.Operations.Reference
{
    public interface IClosedIntervalUnion<TPoint, TInterval> : IEnumerable<TInterval>
        where TPoint : IComparable<TPoint>
        where TInterval : IClosedInterval<TPoint>
    {
    }
}