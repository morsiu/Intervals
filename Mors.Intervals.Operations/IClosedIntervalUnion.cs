using System;
using System.Collections.Generic;

namespace Mors.Intervals.Operations
{
    public interface IClosedIntervalUnion<TInterval, TEnumerator, TPoint>
        where TEnumerator : IEnumerator<TInterval>
        where TInterval : IClosedInterval<TPoint>
        where TPoint : IComparable<TPoint>
    {
        TEnumerator GetEnumerator();
    }
}