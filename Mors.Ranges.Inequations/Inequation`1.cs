using System;
using System.Collections.Generic;

namespace Mors.Ranges.Inequations
{
    public readonly struct Inequation<T> : IEquatable<Inequation<T>>
        where T : IComparable<T>
    {
        private readonly IInequation<T> _value;

        internal Inequation(IInequation<T> value) => _value = value;

        public Inequation<T> And(Inequation<T> other) =>
            new Inequation<T>(new And<T>(_value, other._value));

        public Inequation<T> Closure() =>
            new Inequation<T>(new Closure<T>(_value));

        public bool IsEmpty<TPoints>()
            where TPoints : struct, IPoints<T> =>
            new IsInequationEmpty<T, TPoints>(_value).Value();

        public Inequation<T> Not() =>
            new Inequation<T>(new Not<T>(_value));

        public Inequation<T> Or(Inequation<T> other) =>
            new Inequation<T>(new Or<T>(_value, other._value));

        public bool IsSatisfiedBy(T point) =>
            _value.IsSatisfiedBy(point);

        public bool Equals(Inequation<T> other) =>
            _value.Equals(other._value);

        public override bool Equals(object obj) =>
            obj is Inequation<T> other && Equals(other);

        public override int GetHashCode() =>
            _value.GetHashCode();

        public IEnumerable<TRange> ToClosedRanges<TRange, TPoints, TRanges>()
            where TRanges : struct, IClosedRanges<T, TRange>
            where TPoints : struct, IPoints<T> =>
            new ClosedRangesInInequation<T, TRange, TPoints, TRanges>(_value);

        public IEnumerable<TRange> ToOpenRanges<TRange, TPoints, TRanges>()
            where TRanges : struct, IOpenRanges<T, TRange>
            where TPoints : struct, IPoints<T> =>
            new OpenRangesInInequation<T, TRange, TPoints, TRanges>(_value);

        public override string ToString() =>
            _value.ToString();
    }
}