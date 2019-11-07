using System.Collections.Generic;

namespace Mors.Intervals.Inequations
{
    internal sealed class And<T> : IInequation<T>
    {
        private readonly IInequation<T> _first;
        private readonly IInequation<T> _second;

        public And(IInequation<T> first, IInequation<T> second)
        {
            _first = first;
            _second = second;
        }

        public IEnumerable<T> Boundaries() =>
            new UnionOfBoundaries<T>(_first.Boundaries(), _second.Boundaries());

        public override bool Equals(object obj) =>
            obj is And<T> and
            && EqualityComparer<IInequation<T>>.Default.Equals(_first, and._first)
            && EqualityComparer<IInequation<T>>.Default.Equals(_second, and._second);

        public override int GetHashCode()
        {
            var hashCode = -552916506;
            hashCode = hashCode * -1521134295 + EqualityComparer<IInequation<T>>.Default.GetHashCode(_first);
            hashCode = hashCode * -1521134295 + EqualityComparer<IInequation<T>>.Default.GetHashCode(_second);
            return hashCode;
        }

        public bool IsSatisfiedBy(in T value) =>
            _first.IsSatisfiedBy(value) && _second.IsSatisfiedBy(value);

        public override string ToString() => $"({_first}) ∧ ({_second})";
    }
}
