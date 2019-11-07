using System;

namespace Mors.Intervals.Inequations
{
    internal sealed class InequationOfClosedInterval<T, TInterval>
        where T : IComparable<T>
        where TInterval : IClosedInterval<T>
    {
        private readonly TInterval _interval;

        public InequationOfClosedInterval(TInterval interval) => _interval = interval;

        public IInequation<T> Value() =>
            _interval.Empty() switch
            {
                true => EmptyInterval(),
                false => ClosedInterval()
            };

        private IInequation<T> ClosedInterval() =>
            new And<T>(
                new GreaterThanOrEqualToValue<T>(_interval.Start()),
                new LessThanOrEqualToValue<T>(_interval.End()));

        private static IInequation<T> EmptyInterval() =>
            new False<T>();
    }
}