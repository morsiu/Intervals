using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Intervals.Inequations
{
    internal sealed class InequationOfClosedIntervalUnion<T, TInterval>
        where TInterval: IClosedInterval<T>
        where T : IComparable<T>
    {
        private readonly IEnumerable<TInterval> _value;

        public InequationOfClosedIntervalUnion(IEnumerable<TInterval> value)
        {
            _value = value;
        }

        public IInequation<T> Value()
        {
            return _value.Where(x => !x.Empty())
                .Select(x => new InequationOfClosedInterval<T, TInterval>(x).Value())
                .DefaultIfEmpty(new False<T>())
                .Aggregate((a, b) => new Or<T>(a, b));
        }
    }
}