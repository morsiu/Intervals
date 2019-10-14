using System;
using System.Collections.Generic;

namespace Mors.Ranges.Inequations
{
    internal sealed class InequationOfOpenRange<T, TRange>
        where T : IComparable<T>
        where TRange : IOpenRange<T>
    {
        private readonly TRange _range;

        public InequationOfOpenRange(TRange range) => _range = range;

        public IInequation<T> Value() =>
            _range.Empty() switch
            {
                true => EmptyRange(),
                false =>
                    (_range.ClosedStart(), _range.ClosedEnd()) switch
                    {
                        (false, false) => OpenRange(),
                        (false, true) => LeftOpenRange(),
                        (true, false) => RightOpenRange(),
                        (true, true) =>
                            Comparer<T>.Default.Compare(_range.Start(), _range.End()) == 0
                                ? Point(_range.Start())
                                : ClosedRange()
                    },
            };

        private IInequation<T> ClosedRange() =>
            new And<T>(
                new GreaterThanOrEqualToValue<T>(_range.Start()),
                new LessThanOrEqualToValue<T>(_range.End()));

        private IInequation<T> OpenRange() =>
            new And<T>(
                new GreaterThanValue<T>(_range.Start()),
                new LessThanValue<T>(_range.End()));

        private IInequation<T> LeftOpenRange() =>
            new And<T>(
                new GreaterThanValue<T>(_range.Start()),
                new LessThanOrEqualToValue<T>(_range.End()));

        private IInequation<T> RightOpenRange() =>
            new And<T>(
                new GreaterThanOrEqualToValue<T>(_range.Start()),
                new LessThanValue<T>(_range.End()));

        private static IInequation<T> EmptyRange() =>
            new False<T>();

        private static IInequation<T> Point(in T value) =>
            new EqualToValue<T>(value);
    }
}
