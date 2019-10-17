using System;

namespace Mors.Ranges.Inequations
{
    public static class Inequation
    {
        public static Inequation<T> Equal<T>(in T value)
            where T : IComparable<T> =>
            new Inequation<T>(new EqualToValue<T>(value));

        public static Inequation<T> False<T>()
            where T : IComparable<T>  =>
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
    }
}