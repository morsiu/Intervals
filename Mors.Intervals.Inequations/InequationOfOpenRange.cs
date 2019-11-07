using System;
using System.Collections.Generic;

namespace Mors.Intervals.Inequations
{
    internal sealed class InequationOfOpenInterval<T, TInterval>
        where T : IComparable<T>
        where TInterval : IOpenInterval<T>
    {
        private readonly TInterval _interval;

        public InequationOfOpenInterval(TInterval interval) => _interval = interval;

        public IInequation<T> Value() =>
            _interval.Empty() switch
            {
                true => EmptyInterval(),
                false =>
                    (_interval.ClosedStart(), _interval.ClosedEnd()) switch
                    {
                        (false, false) => OpenInterval(),
                        (false, true) => LeftOpenInterval(),
                        (true, false) => RightOpenInterval(),
                        _ =>
                            Comparer<T>.Default.Compare(_interval.Start(), _interval.End()) == 0
                                ? Point(_interval.Start())
                                : ClosedInterval()
                    },
            };

        private IInequation<T> ClosedInterval() =>
            new And<T>(
                new GreaterThanOrEqualToValue<T>(_interval.Start()),
                new LessThanOrEqualToValue<T>(_interval.End()));

        private IInequation<T> OpenInterval() =>
            new And<T>(
                new GreaterThanValue<T>(_interval.Start()),
                new LessThanValue<T>(_interval.End()));

        private IInequation<T> LeftOpenInterval() =>
            new And<T>(
                new GreaterThanValue<T>(_interval.Start()),
                new LessThanOrEqualToValue<T>(_interval.End()));

        private IInequation<T> RightOpenInterval() =>
            new And<T>(
                new GreaterThanOrEqualToValue<T>(_interval.Start()),
                new LessThanValue<T>(_interval.End()));

        private static IInequation<T> EmptyInterval() =>
            new False<T>();

        private static IInequation<T> Point(in T value) =>
            new EqualToValue<T>(value);
    }
}
