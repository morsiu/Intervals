using System.Collections.Generic;

namespace Mors.Ranges.Inequations
{
    internal interface IInequation<T>
    {
        bool IsSatisfiedBy(in T value);

        IEnumerable<T> Boundaries();
    }
}
