using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Inequations
{
    internal sealed class EqualToValue<T> : IInequation<T>
    {
        private readonly T _value;

        public EqualToValue(in T value) => _value = value;

        public IEnumerable<T> Boundaries() => Enumerable.Repeat(_value, 1);

        public override bool Equals(object obj) =>
            obj is EqualToValue<T> value
            && EqualityComparer<T>.Default.Equals(_value, value._value);

        public override int GetHashCode() =>
            -1939223833 + EqualityComparer<T>.Default.GetHashCode(_value);

        public bool IsSatisfiedBy(in T value) => EqualityComparer<T>.Default.Equals(value, _value);

        public override string ToString() => $"x = {_value}";
    }
}
