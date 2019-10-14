using System;
using System.Collections.Generic;

namespace Mors.Ranges.Inequations
{
    public static class Inequation
    {
        public static Inequation<T> And<T>(Inequation<T> first, Inequation<T> second) =>
            new Inequation<T>(new And<T>(first.Value, second.Value));

        public static Inequation<T> Closure<T>(Inequation<T> value) where T : IComparable<T> =>
            new Inequation<T>(new Closure<T>(value.Value));

        public static Inequation<T> Equal<T>(in T value) =>
            new Inequation<T>(new EqualToValue<T>(value));

        public static Inequation<T> False<T>() =>
            new Inequation<T>(new False<T>());

        public static Inequation<T> FromOpenRange<T, TRange>(TRange value)
            where T : IComparable<T>
            where TRange : IOpenRange<T> =>
            new Inequation<T>(new InequationOfOpenRange<T, TRange>(value).Value());

        public static Inequation<T> FromClosedRange<T, TRange>(TRange value)
            where T : IComparable<T>
            where TRange : IClosedRange<T> =>
            new Inequation<T>(new InequationOfClosedRange<T, TRange>(value).Value());

        public static Inequation<T> GreaterThan<T>(in T value) where T : IComparable<T> =>
            new Inequation<T>(new GreaterThanValue<T>(value));

        public static Inequation<T> GreaterThanOrEqualTo<T>(in T value) where T : IComparable<T> =>
            new Inequation<T>(new GreaterThanOrEqualToValue<T>(value));

        public static Inequation<T> LessThan<T>(in T value) where T : IComparable<T> =>
            new Inequation<T>(new LessThanValue<T>(value));

        public static Inequation<T> LessThanOrEqualTo<T>(in T value) where T : IComparable<T> =>
            new Inequation<T>(new LessThanOrEqualToValue<T>(value));

        public static Inequation<T> Not<T>(Inequation<T> value) =>
            new Inequation<T>(new Not<T>(value.Value));

        public static Inequation<T> Or<T>(Inequation<T> first, Inequation<T> second) =>
            new Inequation<T>(new Or<T>(first.Value, second.Value));

        public static IEnumerable<TRange> ToClosedRanges<T, TRange, TPoints, TRanges>(Inequation<T> value)
            where TRanges : struct, IClosedRanges<T, TRange>
            where TPoints : struct, IPoints<T>
            where T : struct, IComparable<T> =>
            new ClosedRangesInInequation<T, TRange, TPoints, TRanges>(value.Value);

        public static IEnumerable<TRange> ToOpenRanges<T, TRange, TPoints, TRanges>(Inequation<T> value)
            where TRanges : struct, IOpenRanges<T, TRange>
            where TPoints : struct, IPoints<T>
            where T : struct, IComparable<T> =>
            new OpenRangesInInequation<T, TRange, TPoints, TRanges>(value.Value);
    }
}