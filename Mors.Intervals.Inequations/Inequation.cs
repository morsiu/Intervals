using System;

namespace Mors.Intervals.Inequations
{
    public static class Inequation
    {
        public static Inequation<T> Equal<T>(in T value)
            where T : IComparable<T> =>
            new Inequation<T>(new EqualToValue<T>(value));

        public static Inequation<T> False<T>()
            where T : IComparable<T>  =>
            new Inequation<T>(new False<T>());

        public static Inequation<T> FromOpenInterval<T, TInterval>(TInterval value)
            where T : IComparable<T>
            where TInterval : IOpenInterval<T> =>
            new Inequation<T>(new InequationOfOpenInterval<T, TInterval>(value).Value());

        public static Inequation<T> FromClosedInterval<T, TInterval>(TInterval value)
            where T : IComparable<T>
            where TInterval : IClosedInterval<T> =>
            new Inequation<T>(new InequationOfClosedInterval<T, TInterval>(value).Value());

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