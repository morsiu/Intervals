using System;

namespace Mors.Ranges.Inequations
{
    internal sealed class InequationOfClosedRange<T, TRange>
        where T : IComparable<T>
        where TRange : IClosedRange<T>
    {
        private readonly TRange _range;

        public InequationOfClosedRange(TRange range) => _range = range;

        public IInequation<T> Value() =>
            _range.Empty() switch
            {
                true => EmptyRange(),
                false => ClosedRange()
            };

        private IInequation<T> ClosedRange() =>
            new And<T>(
                new GreaterThanOrEqualToValue<T>(_range.Start()),
                new LessThanOrEqualToValue<T>(_range.End()));

        private static IInequation<T> EmptyRange() =>
            new False<T>();
    }
}