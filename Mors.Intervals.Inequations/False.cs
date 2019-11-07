using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Inequations
{
    internal sealed class False<T> : IInequation<T>
    {
        public IEnumerable<T> Boundaries() => Enumerable.Empty<T>();

        public override bool Equals(object obj) => obj is False<T>;

        public override int GetHashCode() => 0;

        public bool IsSatisfiedBy(in T value) => false;

        public override string ToString() => "false";
    }
}