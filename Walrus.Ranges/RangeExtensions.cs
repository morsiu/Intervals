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
    }
}
