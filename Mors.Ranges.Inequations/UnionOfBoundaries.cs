using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Inequations
{
    internal sealed class UnionOfBoundaries<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _first;
        private readonly IEnumerable<T> _second;

        public UnionOfBoundaries(IEnumerable<T> first, IEnumerable<T> second)
        {
            _first = first;
            _second = second;
        }

        public IEnumerator<T> GetEnumerator() =>
            _first.Union(_second).OrderBy(x => x).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
