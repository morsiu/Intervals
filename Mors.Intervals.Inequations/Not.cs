using System;
using System.Collections.Generic;

namespace Mors.Intervals.Inequations
{
    internal sealed class Not<T> : IInequation<T>
    {
        private readonly IInequation<T> _inequation;

        public Not(IInequation<T> inequation) => _inequation = inequation;

        public IEnumerable<T> Boundaries() => _inequation.Boundaries();

        public override bool Equals(object? obj) =>
            obj is Not<T> other
            && EqualityComparer<IInequation<T>>.Default.Equals(_inequation, other._inequation);

        public override int GetHashCode() => HashCode.Combine(_inequation);

        public bool IsSatisfiedBy(in T value) => !_inequation.IsSatisfiedBy(value);

        public override string ToString() => $"¬({_inequation})";
    }
}
