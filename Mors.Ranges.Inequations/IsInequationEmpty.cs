using System;
using System.Collections.Generic;
using System.Linq;

namespace Mors.Ranges.Inequations
{
    internal sealed class IsInequationEmpty<T, TPoints>
        where TPoints : struct, IPoints<T>
        where T : IComparable<T>
    {
        private readonly IInequation<T> _inequation;

        public IsInequationEmpty(IInequation<T> inequation) =>
            _inequation = inequation;

        public bool Value()
        {
            var boundaries = _inequation.Boundaries();
            return !boundaries
                .SelectMany(Neighborhood)
                .DefaultIfEmpty(default(TPoints).Minimum())
                .Any(x => _inequation.IsSatisfiedBy(x));

            static IEnumerable<T> Neighborhood(T point)
            {
                var points = default(TPoints);
                var previous = points.Previous(point);
                if (previous.HasValue) yield return previous.Value;
                yield return point;
                var next = points.Next(point);
                if (next.HasValue) yield return next.Value;
            }
        }
    }
}