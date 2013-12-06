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

        public static IRange<T> Open<T>(T start, T end)
            where T : IComparable<T>
        {
            RangeValidator.CheckEndGreaterThanStart(start, end);
            return new Range<T>(start, end, true, true);
        }

        public static IRange<T> LeftOpen<T>(T start, T end)
            where T : IComparable<T>
        {
            RangeValidator.CheckEndGreaterThanStart(start, end);
            return new Range<T>(start, end, true, false);
        }

        public static IRange<T> RightOpen<T>(T start, T end)
            where T : IComparable<T>
        {
            RangeValidator.CheckEndGreaterThanStart(start, end);
            return new Range<T>(start, end, false, true);
        }

        public static IRange<T> LeftClosed<T>(T start, T end)
            where T : IComparable<T>
        {
            return RightOpen(start, end);
        }

        public static IRange<T> RightClosed<T>(T start, T end)
            where T : IComparable<T>
        {
            return LeftOpen(start, end);
        }

        public static IRange<T> Empty<T>()
            where T : IComparable<T>
        {
            return EmptyRange<T>.Value;
        }
    }
}
