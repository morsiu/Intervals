using System;
using System.Collections.Generic;

namespace Mors.Ranges.Inequations
{
    public readonly struct Inequation<T> : IEquatable<Inequation<T>>
        where T : IComparable<T>
    {
        internal Inequation(IInequation<T> value) => Value = value;

        internal IInequation<T> Value { get; }

        public Inequation<T> And(Inequation<T> other) =>
            new Inequation<T>(new And<T>(Value, other.Value));

        public Inequation<T> Closure() =>
            new Inequation<T>(new Closure<T>(Value));

        public Inequation<T> Not() =>
            new Inequation<T>(new Not<T>(Value));

        public Inequation<T> Or(Inequation<T> other) =>
            new Inequation<T>(new Or<T>(Value, other.Value));

        public bool IsSatisfiedBy(T point) =>
            Value.IsSatisfiedBy(point);

        public bool Equals(Inequation<T> other) =>
            Value.Equals(other.Value);

        public override bool Equals(object obj) =>
            obj is Inequation<T> other && Equals(other);

        public override int GetHashCode() =>
            Value.GetHashCode();

        public IEnumerable<TRange> ToClosedRanges<TRange, TPoints, TRanges>()
            where TRanges : struct, IClosedRanges<T, TRange>
            where TPoints : struct, IPoints<T> =>
            new ClosedRangesInInequation<T, TRange, TPoints, TRanges>(Value);

        public IEnumerable<TRange> ToOpenRanges<TRange, TPoints, TRanges>()
            where TRanges : struct, IOpenRanges<T, TRange>
            where TPoints : struct, IPoints<T> =>
            new OpenRangesInInequation<T, TRange, TPoints, TRanges>(Value);

        public override string ToString() =>
            Value.ToString();
    }
}