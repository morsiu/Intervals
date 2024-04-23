using System;
using System.Collections.Generic;

namespace Mors.Intervals.Inequations
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

        public override bool Equals(object? obj) =>
            obj is Inequation<T> other && Equals(other);

        public override int GetHashCode() =>
            _value.GetHashCode();

        public IEnumerable<TInterval> ToClosedIntervals<TInterval, TPoints, TIntervals>()
            where TIntervals : struct, IClosedIntervals<T, TInterval>
            where TPoints : struct, IPoints<T> =>
            new ClosedIntervalsInInequation<T, TInterval, TPoints, TIntervals>(_value);

        public IEnumerable<TInterval> ToOpenIntervals<TInterval, TPoints, TIntervals>()
            where TIntervals : struct, IOpenIntervals<T, TInterval>
            where TPoints : struct, IPoints<T> =>
            new OpenIntervalsInInequation<T, TInterval, TPoints, TIntervals>(_value);

        public override string? ToString() =>
            _value.ToString();
    }
}