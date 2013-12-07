using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walrus.Ranges
{
    public static class RangeOperations
    {
        public static bool IntersectsWith<T>(IRange<T> x, IRange<T> y)
            where T : IComparable<T>
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");
            if (x.IsEmpty || y.IsEmpty) return false;
            if (x.Start.CompareTo(y.End) > 0) return false;
            if (x.End.CompareTo(y.Start) < 0) return false;
            if (x.Start.CompareTo(y.End) == 0) return !x.HasOpenStart && !y.HasOpenEnd;
            if (x.End.CompareTo(y.Start) == 0) return !x.HasOpenEnd && !y.HasOpenStart;
            return true;
        }

        public static IRange<T> Intersect<T>(IRange<T> x, IRange<T> y)
            where T : IComparable<T>
        {
            if (!IntersectsWith(x, y)) return Range.Empty<T>();

            var xStartToYStart = x.Start.CompareTo(y.Start);
            var xEndToYEnd = x.End.CompareTo(y.End);

            return Range.Create(
                xStartToYStart > 0 ? x.Start : y.Start,
                xEndToYEnd < 0 ? x.End : y.End,
                xStartToYStart == 0
                    ? x.HasOpenStart || y.HasOpenStart
                    : xStartToYStart > 0
                        ? x.HasOpenStart
                        : y.HasOpenStart,
                xEndToYEnd == 0
                    ? x.HasOpenEnd || y.HasOpenEnd
                    : xEndToYEnd < 0
                        ? x.HasOpenEnd
                        : y.HasOpenEnd);
        }

        public static bool Covers<T>(IRange<T> x, IRange<T> y)
            where T : IComparable<T>
        {
            if (!IntersectsWith(x, y)) return false;

            var xStartToYStart = x.Start.CompareTo(y.Start);
            var xEndToYEnd = x.End.CompareTo(y.End);

            return (xStartToYStart < 0 || (xStartToYStart == 0 && (!x.HasOpenStart || (x.HasOpenStart && y.HasOpenStart))))
                && (xEndToYEnd > 0 || (xEndToYEnd == 0 && (!x.HasOpenEnd || (x.HasOpenEnd && y.HasOpenEnd))));
        }
    }
}
