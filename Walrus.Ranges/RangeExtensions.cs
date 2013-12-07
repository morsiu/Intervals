using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus.Ranges
{
    public static class RangeExtensions
    {
        public static bool IntersectsWith<T>(this IRange<T> range, IRange<T> other)
            where T : IComparable<T>
        {
            return RangeOperations.IntersectsWith(range, other);
        }

        public static IRange<T> Intersect<T>(this IRange<T> range, IRange<T> other)
            where T : IComparable<T>
        {
            return RangeOperations.Intersect(range, other);
        }

        public static bool Covers<T>(this IRange<T> range, IRange<T> other)
            where T : IComparable<T>
        {
            return RangeOperations.Covers(range, other);
        }

        public static bool IsCoveredBy<T>(this IRange<T> range, IRange<T> other)
            where T : IComparable<T>
        {
            return RangeOperations.IsCoveredBy(range, other);
        }
    }
}
