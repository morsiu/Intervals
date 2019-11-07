using System.Collections.Generic;

namespace Mors.Intervals.Inequations
{
    internal interface IInequation<T>
    {
        bool IsSatisfiedBy(in T value);

        IEnumerable<T> Boundaries();
    }
}
