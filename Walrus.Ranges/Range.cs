using System;
namespace Walrus.Ranges
{
    public static class Range
    {
        public static IRange<T> Closed<T>(T start, T end)
            where T : IComparable<T>
        {
            RangeValidator.CheckStartNotGreaterThanEnd(start, end);
            return new Range<T>(start, end, false, false);
        }
    }
}
