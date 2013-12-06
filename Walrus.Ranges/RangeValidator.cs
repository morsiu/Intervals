using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus.Ranges
{
    internal static class RangeValidator
    {
        public static void CheckStartNotGreaterThanEnd<T>(T start, T end)
            where T : IComparable<T>
        {
            if (start.CompareTo(end) > 0)
            {
                throw new ArgumentException("Value of range start cannot be greater than value of range end.");
            }
        }

        public static void CheckEndGreaterThanStart<T>(T start, T end)
            where T : IComparable<T>
        {
            if (start.CompareTo(end) >= 0)
            {
                throw new ArgumentException("Value of range end must be greater than value of range start.");
            }
        }
    }
}
