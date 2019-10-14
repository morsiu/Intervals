using System;

namespace Mors.Ranges.Inequations
{
    public readonly struct Inequation<T> : IEquatable<Inequation<T>>
    {
        internal Inequation(IInequation<T> value) => Value = value;

        internal IInequation<T> Value { get; }

        public bool IsSatisfiedBy(T point) =>
            Value.IsSatisfiedBy(point);

        public bool Equals(Inequation<T> other) =>
            Value.Equals(other.Value);

        public override bool Equals(object obj) =>
            obj is Inequation<T> other && Equals(other);

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() =>
            Value.ToString();
    }
}