using System;
using System.Collections.Generic;

namespace Mors.Intervals.Inequations
{
    internal sealed class Or<T> : IInequation<T>
    {
        private readonly IInequation<T> _first;
        private readonly IInequation<T> _second;

        public Or(IInequation<T> first, IInequation<T> second)
        {
            _first = first;
            _second = second;
        }

        public IEnumerable<T> Boundaries() =>
            new UnionOfBoundaries<T>(_first.Boundaries(), _second.Boundaries());

        public override bool Equals(object? obj) =>
            obj is Or<T> or
            && EqualityComparer<IInequation<T>>.Default.Equals(_first, or._first)
            && EqualityComparer<IInequation<T>>.Default.Equals(_second, or._second);

        public override int GetHashCode() => HashCode.Combine(_first, _second);

        public bool IsSatisfiedBy(in T value) =>
            _first.IsSatisfiedBy(value) || _second.IsSatisfiedBy(value);

        public override string ToString() => $"({_first}) ∨ ({_second})";
    }
}
