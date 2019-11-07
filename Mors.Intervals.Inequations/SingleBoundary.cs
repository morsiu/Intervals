using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Inequations
{
    internal sealed class SingleBoundary<T> : IEnumerable<T>
    {
        private readonly T _value;

        public SingleBoundary(T value) => _value = value;

        public IEnumerator<T> GetEnumerator() =>
            Enumerable.Repeat(_value, 1).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
